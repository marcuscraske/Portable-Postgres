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
using System.Xml;

namespace Portable_Postgres
{
    public partial class Form1 : Form
    {
        // Version variables used to check if the current build is the latest etc
        private const int versionMajor = 1;
        private const int versionMin = 3;
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
        private const string updateDownloadURL = "https://raw.github.com/ubermeat/Portable-Postgres/master/Binaries/Download.zip";
        /// <summary>
        /// Used to store the process responsible for launching the Postgres server process; we can also use this to stop the
        /// server from being launched again etc.
        /// </summary>
        private Process p = null;
        /// <summary>
        /// Responsible for managing the download of the Postgres database server.
        /// </summary>
        private WebClient wb = null;
        /// <summary>
        /// Used to store the settings for the user.
        /// </summary>
        private XmlDocument settings = null;
        /// <summary>
        /// To avoid the TextChanged events from firing a save when being set text from the settings file.
        /// </summary>
        private bool formLoaded = false;
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
            // Check if settings file exists
            if (File.Exists(Environment.CurrentDirectory + "\\Settings.xml"))
            {
                // Load settings file
                string data = File.ReadAllText(Environment.CurrentDirectory + "\\Settings.xml");
                settings = new XmlDocument();
                settings.LoadXml(data);
            }
            else
            {
                // Create settings file
                StringBuilder sb = new StringBuilder();
                XmlWriter xw = XmlWriter.Create(sb);
                // Start le document
                xw.WriteStartDocument();
                // Write first element
                xw.WriteStartElement("settings");
                // --- CONFIG

                // Write db usr, pass and db settings
                xw.WriteStartElement("user");                   // Client username
                xw.WriteCData("User");
                xw.WriteEndElement();

                xw.WriteStartElement("pass");                   // Client password
                xw.WriteCData("");
                xw.WriteEndElement();

                xw.WriteStartElement("db");                     // Client database
                xw.WriteCData("postgres");
                xw.WriteEndElement();

                xw.WriteStartElement("path");                   // Client path
                xw.WriteCData(Environment.CurrentDirectory);
                xw.WriteEndElement();

                // --- END OF CONFIG
                // Write end element
                xw.WriteEndElement();
                // End the document
                xw.WriteEndDocument();
                xw.Close();
                // Write the settings to file
                File.WriteAllText(Environment.CurrentDirectory + "\\Settings.xml", sb.ToString());
                // Set the settings heap variable
                settings = new XmlDocument();
                settings.LoadXml(sb.ToString());
            }
            // Load client textbox with path
            pathSQL.Text = settings != null ? settings["settings"]["path"].InnerText : Environment.CurrentDirectory;
            // Load client user, pass and db textboxes
            dbUser.Text = settings != null ? settings["settings"]["user"].InnerText : "User";
            dbPass.Text = settings != null ? settings["settings"]["pass"].InnerText : "";
            dbDatabase.Text = settings != null ? settings["settings"]["db"].InnerText : "postgres";
            // Attach version information
            Text += " - v" + versionMajor + "." + versionMin + "." + versionBuild;
            versionText.Text = "v" + versionMajor + "." + versionMin + "." + versionBuild;
            // Launch updater thread
            Thread th = new Thread(new ParameterizedThreadStart(updateCheck));
            th.Start();
            // Form has loaded
            formLoaded = true;
        }
        /// <summary>
        /// Executed when the form is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            // Check if postgres has been downloaded
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Postgres"))
                // Show the download group
                controlsShowDownload();
            else
                // Show the launch group
                controlsShowLaunch();
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
            statusText.Text = e.BytesReceived + " bytes / " + e.TotalBytesToReceive + " bytes (" + e.ProgressPercentage + "%)";
            progressBar1.Value = e.ProgressPercentage;
        }
        /// <summary>
        /// Invoked when the download of the Postgres files has been completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void  wb_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            buttDownloadAbort.Visible = false;
            progressBar1.Value = 0;
            if (e.Cancelled || e.Error != null)
                MessageBox.Show("Failed to download Postgres!", "Failed...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // Invoke installation thread
                threadInstallRun();
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
                MessageBox.Show("The database for the Postgres server will now be initialized...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Kill any processes remaining
            killAllProcesses();
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
                p.StartInfo.Arguments = "/c \"\"" + Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin\\psql.exe\" " + (dbDatabase.TextLength != 0 ? " " + dbDatabase.Text + (dbUser.TextLength != 0 ? " " + dbUser.Text + (dbPass.TextLength != 0 ? " " + dbPass.Text : "") : "") : "") + "\"";
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
        /// <summary>
        /// Group 2 - used to wipe the Postgres installation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you absolutely sure you want to delete all your Postgres files?\n\nThis will also delete your Postgres database, make sure you have all your SQL saved!", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.OK)
            {
                Thread th = new Thread(new ParameterizedThreadStart(threadWipeDb));
                th.Start(this);
            }
        }
        /// <summary>
        /// Shows the
        /// </summary>
        public void controlsShowLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Visible = true;
                groupDownload.Visible = false;
            });
        }
        public void controlsShowDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Visible = true;
                groupLaunch.Visible = false;
            });
        }
        public void controlsEnableLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Enabled = true;
            });
        }
        public void controlsDisableLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Enabled = false;
            });
        }
        public void controlsEnableDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Enabled = true;
            });
        }
        public void controlsDisableDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Enabled = false;
            });
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
            // Create database
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "initdb.exe";
                p.StartInfo.Arguments = "-D \"" + Environment.CurrentDirectory + "\\Postgres\\Database\"";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit();
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
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create database user:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Wipes the Postgres installation.
        /// </summary>
        void wipe()
        {
            // Disable launch group
            controlsDisableLaunch();
            // Kill any server processes
            killAllProcesses();
            // Delete files
            try
            { Directory.Delete(Environment.CurrentDirectory + "\\Postgres", true); }
            catch { }
            // Enable launch group
            controlsEnableLaunch();
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
            catch { }
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
                // We use invoke in-case the installer is invoking this method from another thread not in-sync with the
                // controls
                Invoke((MethodInvoker)delegate()
                {
                    lsStart.Visible = false;
                    lsStop.Visible = true;
                });
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

        #region "Methods - Settings & Control Value Changed Event Handlers"
        /// <summary>
        /// For updating any configuration keys; this does not instantly save, rather a timer is started to let
        /// the user update other properties for ultimate I/O efficiency.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void updateSettings(string key, string value)
        {
            if (!formLoaded) return;
            // Update setting
            settings["settings"][key].InnerText = value;
            // Trigger save timer - this will save any input after the user has stopped typing for 3s
            saveSettings.Enabled = true;
            // Reset the timer
            saveSettings.Stop();
            saveSettings.Start();
        }
        /// <summary>
        /// Responsible for saving the configuration; read description of updateSettings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveSettings_Tick(object sender, EventArgs e)
        {
            // -- Save the settings
            // Disable timer
            saveSettings.Enabled = false;
            // Build text
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            settings.WriteTo(xw);
            xw.Close();
            // Write to file
            File.WriteAllText(Environment.CurrentDirectory + "\\Settings.xml", sb.ToString());
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Settings configuration file saved!");
#endif
        }
        // Event handlers for value changing
        private void pathSQL_TextChanged(object sender, EventArgs e)
        {
            updateSettings("path", pathSQL.Text);
        }
        private void dbUser_TextChanged(object sender, EventArgs e)
        {
            updateSettings("user", dbUser.Text);
        }
        private void dbPass_TextChanged(object sender, EventArgs e)
        {
            updateSettings("pass", dbPass.Text);
        }
        private void dbDatabase_TextChanged(object sender, EventArgs e)
        {
            updateSettings("db", dbDatabase.Text);
        }
        #endregion

        #region "Methods - Installation Process"
        public void threadInstallRun()
        {
            Thread th = new Thread(new ParameterizedThreadStart(threadInstall));
            th.Start(this);
        }
        public static void threadInstall(object o)
        {
            Form1 form = (Form1)o;
            // Extract zip file
            form.installUpdateProgress("Extracting server files...", 10);
            try
            {
                ZipFile f = new ZipFile(Environment.CurrentDirectory + "\\Postgres.zip");
                f.ExtractAll(Environment.CurrentDirectory + "\\Postgres");
                f.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to decompress zip-file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.installUpdateProgress("Failed to extract zip-file - possibly a corrupt download...", 0);
                form.threadInstallEnableDownloading();
                return;
            }
            // Delete zip file
            form.installUpdateProgress("Deleting zip file...", 20);
            try
            { File.Delete(Environment.CurrentDirectory + "\\Postgres.zip"); }
            catch { }
            // Initialize database
            form.installUpdateProgress("Initializing database structure...", 30);
            if (form.initDatabase())
            {
                form.installUpdateProgress("Idle", 0);
                MessageBox.Show("Installation finished, your server is now running!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form.Invoke((MethodInvoker)delegate()
                {
                    // Show launching group
                    form.controlsShowLaunch();
                });
            }
            else
            {
                form.installUpdateProgress("Failed to init database!", 0);
                form.threadInstallEnableDownloading();
            }
        }
        public static void threadWipeDb(object o)
        {
            Form1 form = (Form1)o;
            // Show download group but disable abort and download buttons
            form.controlsShowDownload();
            form.buttDownload.Enabled = false;
            form.buttDownloadAbort.Enabled = false;

            // Execute the method
            form.wipe();

            // Show the launch group
            form.controlsShowLaunch();

            // Enable the download and abort buttons
            form.buttDownload.Enabled = true;
            form.buttDownloadAbort.Enabled = true;

        }
        public void installUpdateProgress(string text, int installProgress)
        {
            Invoke((MethodInvoker)delegate()
            {
                statusText.Text = text;
                installationProgress.Value = installProgress;
            });
        }
        /// <summary>
        /// Enables the download URL box and button.
        /// </summary>
        void threadInstallEnableDownloading()
        {
            Invoke((MethodInvoker)delegate()
            {
                buttDownload.Visible = true;
                comboBox1.Enabled = true;
            });
        }
        #endregion
    }
}