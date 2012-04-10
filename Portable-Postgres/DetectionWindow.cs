using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace Portable_Postgres
{
    public partial class DetectionWindow : Form
    {
        #region "Variables"
        private bool processAlreadyRunning = false;
        private bool serviceRunning = false;
        private bool portUsed = false;
        #endregion

        #region "Constructor"
        public DetectionWindow()
        {
            InitializeComponent();
            // Perform checks
            // -- Process already running
            processAlreadyRunning = Process.GetProcessesByName("postgres").Length > 0 || Process.GetProcessesByName("initdb").Length > 0;
            // -- Service checks
            foreach (ServiceController service in ServiceController.GetServices())
                if (service.ServiceName.ToLower().Contains("postgres") && service.Status != ServiceControllerStatus.Stopped)
                {
                    serviceRunning = true;
                    break;
                }
            // -- Port used
            // -- / -- Test by trying to bind the port for listening
            try
            {
                TcpListener sock = new TcpListener(System.Net.IPAddress.Any, 5432);
                sock.Start();
                sock.Stop();
            }
            catch (Exception ex)
            {
                portUsed = true;
            }
            // -- / -- Test by trying to connect to the port
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(System.Net.IPAddress.Any, 5432);
                portUsed = true;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region "Methods - Events"
        private void DetectionWindow_Load(object sender, EventArgs e)
        {
            // Check if any conflicts have been discovered, else close the form
            if (!processAlreadyRunning && !serviceRunning && !portUsed)
                Close();
            // Hide the panels for the issues of which do not exist
            panelProcessRunning.Visible = processAlreadyRunning;
            panelServiceRunning.Visible = serviceRunning;
            panelPort.Visible = portUsed;
        }
        /// <summary>
        /// Invoked when the user clicks the ignore button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttIgnore_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Invoked when the user clicks the Terminate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttTerminate_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("postgres"))
                p.Kill();
            foreach (Process p in Process.GetProcessesByName("initdb"))
                p.Kill();
        }
        /// <summary>
        /// Invoked when the user clicks the Stop Service button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttStopService_Click(object sender, EventArgs e)
        {
            foreach (ServiceController service in ServiceController.GetServices())
                if (service.ServiceName.Contains("postgres") && service.Status != ServiceControllerStatus.Stopped)
                    service.Stop();
        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Indicates if any conflicts were found.
        /// </summary>
        /// <returns></returns>
        public bool conflictsFound()
        {
            return processAlreadyRunning || serviceRunning || portUsed;
        }
        #endregion
    }
}
