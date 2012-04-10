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
using Microsoft.Win32;

namespace Portable_Postgres
{
    public partial class Main : Form
    {
        #region "Constants
            #region "Versioning"
            // Version constants used to check if the current build is the latest etc
            private const int versionMajor = 1;
            private const int versionMin = 5;
            private const int versionBuild = 0;
            #endregion
            #region "Settings keynames"
            private const string constSettingsClientPath = "client_path";
            private const string constSettingsClientUser = "client_user";
            private const string constSettingsClientPass = "client_pass";
            private const string constSettingsClientDb = "client_db";
            private const string constSettingsHideServerWindow = "hide_server_window";
            private const string constSettingsAutomaticallyLaunch = "automatically_launch";
            #endregion
            #region "pgAdmin 3"
            private const string pgAdmin3_ServersKey = @"Software\pgAdmin III\Servers";
            private static string pgAdmin3_PasswordDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PostgreSQL";
            #endregion
        #endregion

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
        public Main()
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
                xw.WriteStartElement(constSettingsClientUser);                  // Client username
                xw.WriteCData("User");
                xw.WriteEndElement();

                xw.WriteStartElement(constSettingsClientPass);                  // Client password
                xw.WriteCData("");
                xw.WriteEndElement();

                xw.WriteStartElement(constSettingsClientDb);                    // Client database
                xw.WriteCData("postgres");
                xw.WriteEndElement();

                xw.WriteStartElement(constSettingsClientPath);                  // Client path
                xw.WriteCData(Environment.CurrentDirectory);
                xw.WriteEndElement();

                // Write checkbox settings
                xw.WriteStartElement(constSettingsHideServerWindow);            // Hide server window
                xw.WriteCData("1");
                xw.WriteEndElement();

                xw.WriteStartElement(constSettingsAutomaticallyLaunch);         // Automatically launch
                xw.WriteCData("1");
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

                // Show the install dialogue too
                new NewInstall().Show();
            }
            // Load client textbox with path
            pathSQL.Text = settings != null && settings["settings"][constSettingsClientPath] != null ? settings["settings"][constSettingsClientPath].InnerText : Environment.CurrentDirectory;
            // Load client user, pass and db textboxes
            dbUser.Text = settings != null && settings["settings"][constSettingsClientUser] != null ? settings["settings"][constSettingsClientUser].InnerText : "User";
            dbPass.Text = settings != null && settings["settings"][constSettingsClientPass] != null ? settings["settings"][constSettingsClientPass].InnerText : "";
            dbDatabase.Text = settings != null && settings["settings"][constSettingsClientDb] != null ? settings["settings"][constSettingsClientDb].InnerText : "postgres";
            // Set the checkboxes
            lsHide.Checked = settings["settings"][constSettingsHideServerWindow] != null && settings != null && settings["settings"][constSettingsHideServerWindow].InnerText.Equals("1");
            lsAutoLaunch.Checked = settings != null && settings["settings"][constSettingsAutomaticallyLaunch] != null && settings["settings"][constSettingsAutomaticallyLaunch].InnerText.Equals("1");
            // Attach version information
            Text += " - v" + versionMajor + "." + versionMin + "." + versionBuild;
            versionText.Text = "v" + versionMajor + "." + versionMin + "." + versionBuild;
            // Launch updater thread
            Thread th = new Thread(new ParameterizedThreadStart(updateCheck));
            th.Start(this);
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
            // Launch the detection window for diagnostics
            DetectionWindow detect = new DetectionWindow();
            if(detect.conflictsFound()) detect.Show();
            // Check if postgres has been downloaded
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Postgres"))
                // Show the download group
                controlsShowDownload();
            else
            {
                // Show the launch group
                controlsShowLaunch();
                if (!detect.conflictsFound()) // We will only automatically launch if no conflicts were found
                {
                    // Check if to automatically launch the server
                    if (lsAutoLaunch.Checked)
                        launchPostgresServer();
                }
            }
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
            // Check if the server is running - if so, stop it
            if (p != null)
                stopPostgresServer();
            // Launch wiping process in a different thread to the one being used for UI to stop the program from becoming unresponsive
            Thread thread = new Thread(new ParameterizedThreadStart(threadWipeDb));
            thread.Start(this);
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
            // Force saving of settings
            if (saveSettings.Enabled)
            {
                saveSettings.Enabled = false;
                settingsSave();
            }
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
                Thread th = new Thread(new ParameterizedThreadStart(threadWipePostgres));
                th.Start(this);
            }
        }
        /// <summary>
        /// Invoked when the user clicks the help hyper-link label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpWindow hw = new HelpWindow();
            hw.Show();
        }
        /// <summary>
        /// Shows the launch panel.
        /// </summary>
        public void controlsShowLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Visible = true;
                groupDownload.Visible = false;
            });
        }
        /// <summary>
        /// Shows the download panel.
        /// </summary>
        public void controlsShowDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Visible = true;
                groupLaunch.Visible = false;
            });
        }
        /// <summary>
        /// Enables the launch panel.
        /// </summary>
        public void controlsEnableLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Enabled = true;
            });
        }
        /// <summary>
        /// Disables the launch panel.
        /// </summary>
        public void controlsDisableLaunch()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupLaunch.Enabled = false;
            });
        }
        /// <summary>
        /// Enables the download panel.
        /// </summary>
        public void controlsEnableDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Enabled = true;
            });
        }
        /// <summary>
        /// Disables the download panel.
        /// </summary>
        public void controlsDisableDownload()
        {
            Invoke((MethodInvoker)delegate()
            {
                groupDownload.Enabled = false;
            });
        }
        private void buttPgAdmin3_Click(object sender, EventArgs e)
        {
            // Configuring pgAdmin 3 is not critical code, therefore it can fail regardless...it's just an extra feature; this may fail due
            // to security limitations of the machine being used (which is likely to happen due to being portable and used on e.g. public
            // machines).
            try
            {
                // Decide if to update the settings.ini file if the registry key does not exist
                bool createPortablePostgresKey = false;
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(pgAdmin3_ServersKey, true);
                if (rk == null)
                {
                    // First-time - configure the settings.ini
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Postgres\\pgsql\\pgAdmin III\\Settings.ini", "[Servers]\r\nCount=1\n\nServers/1]\r\nServer=127.0.0.1\nDescription=Portable-Postgres\nPort=5432");
                    using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Postgres\\pgsql\\pgAdmin III\\Settings.ini"))
                    {
                        sw.WriteLine("[Servers]");
                        sw.WriteLine("Count=1");
                        sw.WriteLine("[Servers/1]");
                        sw.WriteLine("Server=127.0.0.1");
                        sw.WriteLine("Description=Portable-Postgres");
                        sw.WriteLine("Port=5432");
                        sw.Flush();
                        sw.Close();
                    }
                    // Create the subkey for servers and the count key
                    rk = Registry.CurrentUser.CreateSubKey(pgAdmin3_ServersKey);
                    rk.SetValue("Count", 0);
                    rk.Flush();
                    // Set create pp key to true
                    createPortablePostgresKey = true;
                }
                else
                {
                    // Lets see if Portable-Postgres exists, if so update it - else create a new key
                    RegistryKey subKey;
                    bool foundKey = false;
                    foreach (string server in rk.GetSubKeyNames())
                    {
                        subKey = rk.OpenSubKey(server, true);
                        if (subKey.GetValue("Description").Equals("Portable-Postgres"))
                        {
                            foundKey = true;
                            // Update the server details
                            subKey.SetValue("Username", dbUser.Text);
                            subKey.SetValue("Database", dbDatabase.Text);
                            subKey.SetValue("StorePwd", "true");
                            subKey.SetValue("LastDatabase", dbDatabase.Text);
                            subKey.Flush();
                        }
                    }
                    // Key was not found - create it (probably been renamed must we dont want to break the users custom config so it's better/safer to create new)
                    if (!foundKey) createPortablePostgresKey = true;
                }
                // Check if to create the server key
                if (createPortablePostgresKey)
                {
                    // Grab the number of servers
                    int serverCount = (int)rk.GetValue("Count");
                    // Create the server key
                    RegistryKey nKey = rk.CreateSubKey((serverCount + 1).ToString());
                    nKey.SetValue("Colour", "#FFFFFF");
                    nKey.SetValue("Database", dbDatabase.Text);
                    nKey.SetValue("DbRestriction", "");
                    nKey.SetValue("Description", "Portable-Postgres");
                    nKey.SetValue("DiscoveryID", "");
                    nKey.SetValue("Group", "Servers");
                    nKey.SetValue("HostAddr", "");
                    nKey.SetValue("LastDatabase", dbDatabase.Text);
                    nKey.SetValue("LastSchema", "");
                    nKey.SetValue("Port", 5432);
                    nKey.SetValue("Restore", "true");
                    nKey.SetValue("Rolename", "");
                    nKey.SetValue("Server", "127.0.0.1");
                    nKey.SetValue("Service", "");
                    nKey.SetValue("ServiceID", "");
                    nKey.SetValue("SSL", 0);
                    nKey.SetValue("SSLCert", "");
                    nKey.SetValue("SSLCrl", "");
                    nKey.SetValue("SSLKey", "");
                    nKey.SetValue("SSLRootCert", "");
                    nKey.SetValue("StorePwd", "true");
                    nKey.SetValue("Username", dbUser.Text);
                    nKey.Flush();
                    // Increment the count by one
                    rk.SetValue("Count", serverCount + 1);
                }
                // Check the local folder exists for the password file
                if (!Directory.Exists(pgAdmin3_PasswordDir)) Directory.CreateDirectory(pgAdmin3_PasswordDir);
                // Write the stored password file
                File.WriteAllText(pgAdmin3_PasswordDir + "\\pgpass.conf", "127.0.0.1:5432:*:" + dbUser.Text + ":" + dbPass.Text);
            }
            catch { }
            // Launch Portable Postgres 3
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\Postgres\\pgsql\\bin\\pgAdmin3.exe");
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
            catch (Exception ex) { MessageBox.Show("Error wiping Postgres directory:\n\n" + ex.Message + "\n\nFailed to complete operation, probably a file-lock - make sure no clients are open, else log-off and log-on again to resolve the issue!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                if (currVersionMajor != versionMajor || currVersionMin != versionMin || currVersionBuild != versionBuild)
                {
                    // Show the update window
                    Main main = (Main)o;
                    main.Invoke((MethodInvoker)delegate()
                    {
                        UpdateAvailable ua = new UpdateAvailable(versionMajor + "." + versionMin + "." + versionBuild, currVersionMajor + "." + currVersionMin + "." + currVersionBuild);
                        ua.Show();
                    });
                }
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
            if (settings["settings"][key] != null)
                settings["settings"][key].InnerText = value;
            else
            {
                XmlElement node = settings.CreateElement(key);
                node.InnerText = value;
                settings["settings"].AppendChild(node);
            }
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
            // Save settings
            settingsSave();
        }
        private void settingsSave()
        {
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
            updateSettings(constSettingsClientPath, pathSQL.Text);
        }
        private void dbUser_TextChanged(object sender, EventArgs e)
        {
            updateSettings(constSettingsClientUser, dbUser.Text);
        }
        private void dbPass_TextChanged(object sender, EventArgs e)
        {
            updateSettings(constSettingsClientPass, dbPass.Text);
        }
        private void dbDatabase_TextChanged(object sender, EventArgs e)
        {
            updateSettings(constSettingsClientDb, dbDatabase.Text);
        }
        /// <summary>
        /// Invoked when the checkbox control for "Hide server window" is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsHide_CheckedChanged(object sender, EventArgs e)
        {
            updateSettings(constSettingsHideServerWindow, lsHide.Checked ? "1" : "0");
        }
        /// <summary>
        /// Invoked when the checkbox control for "Automatically launch" is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsAutoLaunch_CheckedChanged(object sender, EventArgs e)
        {
            updateSettings(constSettingsAutomaticallyLaunch, lsAutoLaunch.Checked ? "1" : "0");
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
            Main form = (Main)o;
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
        /// <summary>
        /// Wipes the entire Postgres installation.
        /// </summary>
        /// <param name="o"></param>
        public static void threadWipePostgres(object o)
        {
            Main form = (Main)o;
            form.Invoke((MethodInvoker)delegate()
            {
                // Show download group but disable abort and download buttons
                form.controlsShowDownload();
                form.buttDownload.Enabled = false;
                form.buttDownloadAbort.Enabled = false;
            });
            // Execute the method
            form.wipe();
            // Show the launch group
            form.controlsShowLaunch();
            form.Invoke((MethodInvoker)delegate()
            {
                // Enable the download and abort buttons
                form.buttDownload.Enabled = true;
                form.buttDownloadAbort.Enabled = true;
            });

        }
        /// <summary>
        /// Wipes the database used by the Postgres server.
        /// </summary>
        /// <param name="o"></param>
        public static void threadWipeDb(object o)
        {
            Main form = (Main)o;
            // Disable launch group
            form.controlsDisableLaunch();
            // Kill any potential processes
            form.killAllProcesses();
            // Delete the database directory if it exists
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
            // Reinitialize the database
            form.initDatabase();
            // Re-enable launch group
            form.controlsEnableLaunch();
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