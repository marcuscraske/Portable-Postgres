/*
 * Creative Commons Attribution-ShareAlike 3.0 unported
 * ***************************************************************
 * Author:  limpygnome
 * E-mail:  limpygnome@gmail.com
 * Site:    ubermeat.co.uk
 * ***************************************************************
 * Credit to:
 * -- http://dotnetzip.codeplex.com/ - DotNetZip library
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace Portable_Postgres
{
    public partial class Form1 : Form
    {
        // Version variables used to check if the current build is the latest etc
        private const int versionMajor = 1;
        private const int versionMin = 0;
        private const int versionBuild = 0;

        #region "Variables"
        /// <summary>
        /// Contains the URL for the Github repository.
        /// </summary>
        private const string githubURL = "https://github.com/ubermeat/Portable-Postgres";
        /// <summary>
        /// URL of the version file used to determine if a new version has been released.
        /// </summary>
        private const string updateURL = "https://raw.github.com/ubermeat/Portable-Postgres/master/VERSION";
        /// <summary>
        /// URL of where the binaries of the application can be downloaded.
        /// </summary>
        private const string updateDownloadURL = "https://raw.github.com/ubermeat/Portable-Postgres/tree/master/Binaries/Download.zip";
        /// <summary>
        /// Used to store the process responsible for launching the Postgres server process; we can also use this to stop the
        /// server from being launched again etc.
        /// </summary>
        private Process p = null;
        /// <summary>
        /// Responsible for managing the download of the Postgres database server.
        /// </summary>
        private WebClient wb = null;
        #endregion

        #region "Methods - Event Handlers & Controls"
        /// <summary>
        /// Executed when the form is invoked on start-up.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // Select a download URL based on the OS architecture; we can detect this by
            // checking the length of a pointer; credit goes to:
            // http://www.codeproject.com/Tips/107866/32-Bit-or-64-bit-OS
            if (IntPtr.Size == 4)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = 1;
            // Load client textbox with path
            pathSQL.Text = Environment.CurrentDirectory;
            // Attach version information
            Text += " - v" + versionMajor + "." + versionMin + "." + versionBuild;
            // Launch updater thread
            Thread th = new Thread(new ParameterizedThreadStart(updateCheck));
            th.Start();
        }
        /// <summary>
        /// Group 1 - causes the Postgres database files to be downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttDownload_Click_1(object sender, EventArgs e)
        {
            if (p != null)
            {
                MessageBox.Show("You cannot redownload the Postgres installation whilst the server is running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Directory.Exists(Environment.CurrentDirectory + "\\Postgres"))
                if (MessageBox.Show("An existing installation already exists, you need to delete it to continue the download...", "Existing Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        Directory.Delete(Environment.CurrentDirectory + "\\Postgres", true);
                    }
                    catch
                    {
                        MessageBox.Show("Failed to delete existing installation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                    return;
            if (comboBox1.Text.Length == 0)
                MessageBox.Show("You need to select an item to download!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    if (wb != null)
                    {
                        wb.CancelAsync();
                        wb.Dispose();
                    }
                    wb = new WebClient();
                    wb.DownloadFileCompleted += new AsyncCompletedEventHandler(wb_DownloadFileCompleted);
                    wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);
                    wb.DownloadFileAsync(new Uri(comboBox1.Text), Environment.CurrentDirectory + "\\Postgres.zip");
                    buttDownload.Visible = false;
                    buttDownloadAbort.Visible = true;
                    comboBox1.Enabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to download the following URL:\n'" + comboBox1.Text + "'\n\nError message:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Invoked then the progress of the Postgres server files download has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            statusText.Text = e.BytesReceived + " bytes / " + e.TotalBytesToReceive + " bytes " + e.ProgressPercentage + "%)";
            progressBar1.Value = e.ProgressPercentage;
        }
        /// <summary>
        /// Invoked when the download of the Postgres files has been completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void  wb_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            statusText.Text = "Completed Postgres download! Extracting...";
            buttDownload.Visible = true;
            buttDownloadAbort.Visible = false;
            comboBox1.Enabled = true;
            progressBar1.Value = 0;
            MessageBox.Show("Your Postgres-zip download will now be extracted, the application will freeze for a few seconds...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (e.Cancelled || e.Error != null)
                MessageBox.Show("Failed to download Postgres!", "Failed...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // Extract zip file
                try
                {
                    ZipFile f = new ZipFile(Environment.CurrentDirectory + "\\Postgres.zip");
                    f.ExtractAll(Environment.CurrentDirectory + "\\Postgres");
                    f.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to decompress zip-file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusText.Text = "Failed to extract zip-file - possibly a corrupt download...";
                    return;
                }
                // Delete zip file
                try
                { File.Delete(Environment.CurrentDirectory + "\\Postgres.zip"); }
                catch { }
                // Initialize database
                if (initDatabase())
                {
                    statusText.Text = "Completed, Postgres server is ready and running :3!";
                    MessageBox.Show("Installation finished, your server is now running!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    statusText.Text = "Failed to init database!";
            }
        }
        /// <summary>
        /// Group 1 - aborts downloading the Postgres server files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttDownloadAbort_Click(object sender, EventArgs e)
        {
            if (wb != null)
            {
                wb.CancelAsync();
                wb.Dispose();
                buttDownloadAbort.Visible = false;
                buttDownload.Visible = true;
                comboBox1.Enabled = true;
                progressBar1.Value = 0;
            }
        }
        /// <summary>
        /// Group 2 - starts the database server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsStart_Click(object sender, EventArgs e)
        {
            launchPostgresServer();
        }
        /// <summary>
        /// Group 2 - stops the database server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsStop_Click(object sender, EventArgs e)
        {
            stopPostgresServer();
        }
        /// <summary>
        /// Group 2 - restarts the database server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsRestart_Click(object sender, EventArgs e)
        {
            stopPostgresServer();
            launchPostgresServer();
        }
        /// <summary>
        /// Group 2 - wipes the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsWipe_Click(object sender, EventArgs e)
        {
            if (p != null)
                MessageBox.Show("Cannot wipe the database whilst it is running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // Kill any potential processes
                killAllProcesses();
                // Delete directory
                if (Directory.Exists(Environment.CurrentDirectory + "\\Postgres\\Database"))
                    try
                    {
                        Directory.Delete(Environment.CurrentDirectory + "\\Postgres\\Database", true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to delete database directory:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                // Reinit
                if (initDatabase())
                    MessageBox.Show("Successfully recreated database!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Shuts down the database server when the application is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the server!
            if (p != null)
                try
                {
                    p.Kill();
                }
                catch { }
        }
        /// <summary>
        /// Group 3 - launches the psql client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = pathSQL.Text;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c " + Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin\\psql.exe " + (dbDatabase.TextLength != 0 ? " " + dbDatabase.Text + (dbUser.TextLength != 0 ? " " + dbUser.Text + (dbPass.TextLength != 0 ? " " + dbPass.Text : "") : "") : "");
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch client:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Github contribution link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(githubURL);
        }
        /// <summary>
        /// Group 3 - used to browse for a folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            dbBrowse.ShowDialog();
            if (dbBrowse.SelectedPath.Length != 0)
                pathSQL.Text = dbBrowse.SelectedPath;
        }
        #endregion

        #region "Methods - Postgres"
        /// <summary>
        /// Intializes the database for the database server - this is invoked when wiping or first creating a Postgres
        /// database server.
        /// </summary>
        /// <returns></returns>
        bool initDatabase()
        {
            MessageBox.Show("The database for the Postgres server will now be initialized...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Create database
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "initdb.exe";
                p.StartInfo.Arguments = "-D \"" + Environment.CurrentDirectory + "\\Postgres\\Database\"";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit(60000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create database:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Launch server
            launchPostgresServer();
            // Wait 10s for the postgres server to start - this is heavily flawed but works...most of the time
            System.Threading.Thread.Sleep(10000);
            // Create user
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "createuser.exe";
                p.StartInfo.Arguments = "-s -d -r -e User";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit(3000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create database user:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Stops the Postgres database server..rather dirty.
        /// </summary>
        void stopPostgresServer()
        {
            if (p == null) return;
            // Kill the db launcher
            try
            {
                p.Kill();
                p.Dispose();
            }
            catch
            {
            }
            // Kill the db processes
            killAllProcesses();
            p = null;
            lsStart.Visible = true;
            lsStop.Visible = false;
        }
        /// <summary>
        /// Kills any server processes.
        /// </summary>
        void killAllProcesses()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("postgres"))
                    proc.Kill();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Launches the database server.
        /// </summary>
        void launchPostgresServer()
        {
            if (p != null) return;
            // Kill any processes running
            killAllProcesses();
            // Launch db server
            try
            {
                p = new Process();

                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "pg_ctl.exe";
                p.StartInfo.Arguments = "start -D \"" + Environment.CurrentDirectory + "\\Postgres\\Database" + "\"";
                if (lsHide.Checked) p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                p.Start();

                lsStart.Visible = false;
                lsStop.Visible = true;
            }
            catch (Exception ex)
            {
                // Display error
                p = null;
                MessageBox.Show("Failed to launch Postgres database server:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region "Methods - Update Check"
        public static void updateCheck(object o)
        {
            try
            {
                WebClient wc = new WebClient();
                StreamReader sw = new StreamReader(wc.OpenRead(updateURL));
                // Get the current version
                int currVersionMajor = int.Parse(sw.ReadLine());
                int currVersionMin = int.Parse(sw.ReadLine());
                int currVersionBuild = int.Parse(sw.ReadLine());
                sw.Close();
                wc.Dispose();
                // Compare and ask user
                if ((currVersionMajor != versionMajor || currVersionMin != versionMin || currVersionBuild != versionBuild)
                 && MessageBox.Show("An update is available, would you like to download it?\n\nCurrent version:\n"
                 + versionMajor + "." + versionMin + "." + versionBuild + "\n\nLatest version:\n" + currVersionMajor + "." + currVersionMin + "." + currVersionBuild,
                 "Update Available",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    try
                    {
                        Process.Start(updateDownloadURL);
                    }
                    catch (Exception ex) { MessageBox.Show("Failed to open browser, try manually! Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch(Exception ex)
            {
#if DEBUG
                MessageBox.Show("Update check failed:\n" + ex.Message + "\n\nStack-trace:\n" + ex.StackTrace);
#endif
            }
        }
        #endregion
    }
}
