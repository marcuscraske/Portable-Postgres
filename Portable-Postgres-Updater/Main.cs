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
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.IO;
using Ionic.Zip;

namespace Portable_Postgres_Updater
{
    public partial class Main : Form
    {
        /// <summary>
        /// URL of where the binaries of the application can be downloaded.
        /// </summary>
        private const string updateDownloadURL = "https://raw.github.com/ubermeat/Portable-Postgres/master/Binaries/Download.zip";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            // Begin download
            status(this, "Downloading latest Portable Postgres...");
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadFileAsync(new System.Uri(updateDownloadURL), AppDomain.CurrentDomain.BaseDirectory + "\\Update.zip");
        }
        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Launch the secondary thread responsible for the rest of the process
            Thread t = new Thread(new ParameterizedThreadStart(threadUpdate));
            t.Start(this);
            statusBar(this, 10);
        }
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Update the progressbar
            statusBar(this, e.ProgressPercentage);
            status(this, "Downloading: " + e.ProgressPercentage + "% (" + e.BytesReceived + "/" + e.TotalBytesToReceive + " bytes)");
        }

        public static void threadUpdate(object o)
        {
            Main m = (Main)o;
            // Ensure no Portable-Postgres processes are running
            status(m, "Waiting for Portable-Postgres to terminate...");
            while (true)
            {
                if (Process.GetProcessesByName("Portable-Postgres").Length == 0)
                    break;
                Thread.Sleep(200); // To reduce processing during this period
            }
            // Create temp directory for unzip
            status(m, "Creating temp directory for download...");
            statusBar(m, 30);
            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + "\\__temp";
            if (Directory.Exists(tempFolder))
                try
                { Directory.Delete(tempFolder, true); }
                catch (Exception ex) { throwError(ex.Message); }
            Directory.CreateDirectory(tempFolder);
            // Unzip
            status(m, "Unzipping downloaded data...");
            statusBar(m, 70);
            ZipFile f = new ZipFile(Environment.CurrentDirectory + "\\Update.zip");
            f.ExtractAll(tempFolder);
            f.Dispose();
            // Copy each file exepct the update.exe and Ionic.Zip.Reduced.dll file
            int removeLength = tempFolder.Length;
            string newFileName, newFilePath, newDirectory;
            foreach (string file in Directory.GetFiles(tempFolder))
            {
                newFilePath = AppDomain.CurrentDomain.BaseDirectory + file.Remove(0, removeLength);
                newFileName = Path.GetFileName(newFilePath);
                if (newFileName != "Update.exe" && newFileName != "Ionic.Zip.Reduced.dll") // Check if the filename is to be ignored
                {
                    newDirectory = Path.GetDirectoryName(newFilePath);
                    // Check the directory exists
                    try
                    {
                        if (!Directory.Exists(newDirectory))
                            Directory.CreateDirectory(newDirectory);
                    }
                    catch (Exception ex)
                    { throwError(ex.Message); }
                    // Copy the file
                    try
                    { File.Copy(file, newFilePath, true); }
                    catch (Exception ex) { throwError(ex.Message); }
                }
            }
            // Delete temp directory
            try
            { Directory.Delete(tempFolder, true); }
            catch (Exception ex) { throwError(ex.Message); }
            // Launch Portable-Postgres
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\Portable-Postgres.exe");
            // Exit
            Application.Exit();
        }
        static void throwError(string text)
        {
            MessageBox.Show("An error occurred:\n\n" + text + "\n\nApologies!", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static void status(Main main, string text)
        {
            main.Invoke((MethodInvoker)delegate()
            {
                main.txtStatus.Text = text;
            });
        }
        static void statusBar(Main main, int amount)
        {
            main.Invoke((MethodInvoker)delegate()
            {
                main.progressBar.Value = amount;
            });
        }
    }
}
