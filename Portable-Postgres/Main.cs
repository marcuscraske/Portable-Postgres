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
using Npgsql;

namespace Portable_Postgres
{
    public partial class Main : Form
    {
        #region "Constants"
            #region "Versioning"
            // Version constants used to check if the current build is the latest etc
            private const int VERSION_MAJOR = 1;
            private const int VERSION_MINOR = 6;
            private const int VERSION_BUILD = 1;
            #endregion
            #region "Settings keynames"
            private const string SETTINGS_CLIENT_PATH = "client_path";
            private const string SETTINGS_CLIENT_USER = "client_user";
            private const string SETTINGS_CLIENT_PASS = "client_pass";
            private const string SETTINGS_CLIENT_DB = "client_db";
            private const string SETTINGS_HIDE_SERVER_WINDOW = "hide_server_window";
            private const string SETTINGS_AUTO_LAUNCH = "automatically_launch";
            #endregion
            #region "pgAdmin 3"
            private const string PGADMIN3_REGISTRY_KEY_SERVER = @"Software\pgAdmin III\Servers";
            private static string PGADMIN3_PASSWORD_DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PostgreSQL";
            #endregion
            #region "URLs"
            private const string URL_CONTRIBUTE = @"https://github.com/ubermeat/Portable-Postgres";
            private const string URL_HELP = @"http://ubermeat.co.uk/projects/portable-postgres/help";
            #endregion
        #endregion

        #region "Variables"
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
        /// <summary>
        /// Stores the current instance of the debug console; we only want one at a time, hence
        /// we'll either instantiate this variable or recreate and reset this variable.
        /// </summary>
        DebugConsole debugConsole = null;
        /// <summary>
        /// Used to track the execution of the application for debugging purposes.
        /// </summary>
        Debugger debug;
        /// <summary>
        /// The base directory of this application.
        /// </summary>
        string baseDirectory;
        #endregion

        #region "Methods - Event Handlers & Controls"
        /// <summary>
        /// Executed when the form is invoked on start-up.
        /// </summary>
        public Main()
        {
            InitializeComponent();
            // Set the base path of the application
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if(baseDirectory.EndsWith("/") ||  baseDirectory.EndsWith("\\"))
                baseDirectory = baseDirectory.Substring(0, baseDirectory.Length - 1);
            // -- No longer enabled; switching to an old 32-bit version of Postgres (more stable).
            // Select a download URL based on the OS architecture; we can detect this by
            // checking the length of a pointer; credit goes to:
            // http://www.codeproject.com/Tips/107866/32-Bit-or-64-bit-OS
            //if (IntPtr.Size == 4)
            //    comboBox1.SelectedIndex = 0;
            //else
            //    comboBox1.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;
            // Check if settings file exists
            if (File.Exists(baseDirectory + "\\Settings.xml"))
            {
                // Load settings file
                string data = File.ReadAllText(baseDirectory + "\\Settings.xml");
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
                xw.WriteStartElement(SETTINGS_CLIENT_USER);                  // Client username
                xw.WriteCData("User");
                xw.WriteEndElement();

                xw.WriteStartElement(SETTINGS_CLIENT_PASS);                  // Client password
                xw.WriteCData("");
                xw.WriteEndElement();

                xw.WriteStartElement(SETTINGS_CLIENT_DB);                    // Client database
                xw.WriteCData("postgres");
                xw.WriteEndElement();

                xw.WriteStartElement(SETTINGS_CLIENT_PATH);                  // Client path
                xw.WriteCData(baseDirectory);
                xw.WriteEndElement();

                // Write checkbox settings
                xw.WriteStartElement(SETTINGS_HIDE_SERVER_WINDOW);            // Hide server window
                xw.WriteCData("1");
                xw.WriteEndElement();

                xw.WriteStartElement(SETTINGS_AUTO_LAUNCH);         // Automatically launch
                xw.WriteCData("1");
                xw.WriteEndElement();

                // --- END OF CONFIG
                // Write end element
                xw.WriteEndElement();
                // End the document
                xw.WriteEndDocument();
                xw.Close();
                // Write the settings to file
                File.WriteAllText(baseDirectory + "\\Settings.xml", sb.ToString());
                // Set the settings heap variable
                settings = new XmlDocument();
                settings.LoadXml(sb.ToString());

                // Show the install dialogue too
                new NewInstall().Show();
            }
            // Load client textbox with path
            pathSQL.Text = settings != null && settings["settings"][SETTINGS_CLIENT_PATH] != null ? settings["settings"][SETTINGS_CLIENT_PATH].InnerText : baseDirectory;
            // Load client user, pass and db textboxes
            dbUser.Text = settings != null && settings["settings"][SETTINGS_CLIENT_USER] != null ? settings["settings"][SETTINGS_CLIENT_USER].InnerText : "User";
            dbPass.Text = settings != null && settings["settings"][SETTINGS_CLIENT_PASS] != null ? settings["settings"][SETTINGS_CLIENT_PASS].InnerText : "";
            dbDatabase.Text = settings != null && settings["settings"][SETTINGS_CLIENT_DB] != null ? settings["settings"][SETTINGS_CLIENT_DB].InnerText : "postgres";
            // Set the checkboxes
            lsHide.Checked = settings["settings"][SETTINGS_HIDE_SERVER_WINDOW] != null && settings != null && settings["settings"][SETTINGS_HIDE_SERVER_WINDOW].InnerText.Equals("1");
            lsAutoLaunch.Checked = settings != null && settings["settings"][SETTINGS_AUTO_LAUNCH] != null && settings["settings"][SETTINGS_AUTO_LAUNCH].InnerText.Equals("1");
            // Attach version information
            Text += " - v" + VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_BUILD;
            versionText.Text = "v" + VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_BUILD;
            // Launch the updater to check for le updates with the current PID - to terminate us
            try
            { Process.Start(baseDirectory + "\\Updater.exe"); } catch { }
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
            debug = new Debugger();
            debug.write("Debugger started; base-directory is located at '" + baseDirectory + "'!");
            // Launch the detection window for diagnostics
            DetectionWindow detect = new DetectionWindow();
            if(detect.conflictsFound()) detect.Show();
            // Check if postgres has been downloaded
            string pDir = baseDirectory + "\\Postgres";
            if (!Directory.Exists(pDir))
            {
                // Show the download group
                controlsShowDownload();
                debug.write("Could not find Postgres directory at '" + pDir + "'!");
            }
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
            debug.write("Shown complete.");
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
                error("You cannot redownload the Postgres installation whilst the server is running!");
                return;
            }
            else if (Directory.Exists(baseDirectory + "\\Postgres"))
                if (MessageBox.Show("An existing installation already exists, you need to delete it to continue the download...", "Existing Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        Directory.Delete(baseDirectory + "\\Postgres", true);
                    }
                    catch(Exception ex)
                    {
                        error("Failed to delete existing installation!", ex);
                        return;
                    }
                }
                else
                    return;
            if (comboBox1.Text.Length == 0)
                error("You need to select an item to download!");
            else
            {
                try
                {
                    NewInstallSettings nis = getInstallSettings(this);
                    if (nis == null) return; // No install settings provided, procedure cancelled.
                    if (wb != null)
                    {
                        wb.CancelAsync();
                        wb.Dispose();
                    }
                    wb = new WebClient();
                    wb.DownloadFileCompleted += new AsyncCompletedEventHandler(wb_DownloadFileCompleted);
                    wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);
                    wb.DownloadFileAsync(new Uri(comboBox1.Text), baseDirectory + "\\Postgres.zip", nis);
                    buttDownload.Visible = false;
                    buttDownloadAbort.Visible = true;
                    comboBox1.Enabled = false;
                }
                catch (Exception ex)
                {
                    error("Failed to download the following URL:\r\n'" + comboBox1.Text + "'", ex);
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
            {
                error("Failed to download Postgres!", e.Error);
                debug.write("Failed to download Postgres!");
                Invoke((MethodInvoker)delegate()
                {
                    controlsEnableDownload();
                });
            }
            else
            {
                // Invoke installation thread
                threadInstallRun((NewInstallSettings)e.UserState);
            }
        }
        /// <summary>
        /// Group 1 - aborts downloading the Postgres server files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttDownloadAbort_Click(object sender, EventArgs e)
        {
            debug.write("Aborting download...");
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
            Thread thread = new Thread(delegate()
                {
                    threadWipeDb(this);
                });
            thread.Start();
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
            // Cause application to exit - in-case any other forms are open
            Application.Exit();
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
                p.StartInfo.Arguments = "/c \"\"" + baseDirectory + "\\Postgres\\pgsql\\bin\\psql.exe\" " + (dbDatabase.TextLength != 0 ? " " + dbDatabase.Text + (dbUser.TextLength != 0 ? " " + dbUser.Text : "") : "") + "\"";
                p.Start();
                debug.write("Launched psql client successfully.");
            }
            catch (Exception ex)
            {
                error("Failed to launch client!", ex);
            }
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
            if (MessageBox.Show("Are you absolutely sure you want to delete all your Postgres files?\r\n\r\nThis will also delete your Postgres database, make sure you have all your SQL saved and data backed-up!", "Warning...", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                Thread th = new Thread(
                    delegate()
                    {
                        threadWipePostgres(this);
                    }
                    );
                th.Start();
            }
        }
        /// <summary>
        /// Invoked when the user clicks the help hyper-link label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            launchURL(URL_HELP);
        }
        /// <summary>
        /// Invoked when the user clicks the contribute hyper-link label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkContribute_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(URL_CONTRIBUTE);
        }
        /// <summary>
        /// Invoked when the debug console link-label is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkDebug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (debugConsole != null)
                debugConsole.Dispose();
            debugConsole = new DebugConsole(debug, baseDirectory);
            debugConsole.Show();
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
                debug.write("Showing launch area.");
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
                debug.write("Showing download area.");
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
                debug.write("Enabled launch area.");
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
                debug.write("Disabled launch area.");
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
                debug.write("Enabled download area.");
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
                debug.write("Disabled download area.");
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
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(PGADMIN3_REGISTRY_KEY_SERVER, true);
                if (rk == null)
                {
                    // First-time - configure the settings.ini
                    File.WriteAllText(baseDirectory + "\\Postgres\\pgsql\\pgAdmin III\\Settings.ini", "[Servers]\r\nCount=1\n\nServers/1]\r\nServer=127.0.0.1\nDescription=Portable-Postgres\nPort=5432");
                    using (StreamWriter sw = new StreamWriter(baseDirectory + "\\Postgres\\pgsql\\pgAdmin III\\Settings.ini"))
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
                    rk = Registry.CurrentUser.CreateSubKey(PGADMIN3_REGISTRY_KEY_SERVER);
                    rk.SetValue("Count", 0);
                    rk.Flush();
                    // Set create pp key to true
                    createPortablePostgresKey = true;
                    debug.write("Successfully wrote PgAdmin III first-time setup.");
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
                    debug.write("Added PgAdmin III local server info.");
                }
                // Check the local folder exists for the password file
                if (!Directory.Exists(PGADMIN3_PASSWORD_DIR)) Directory.CreateDirectory(PGADMIN3_PASSWORD_DIR);
                // Write the stored password file
                File.WriteAllText(PGADMIN3_PASSWORD_DIR + "\\pgpass.conf", "127.0.0.1:5432:*:" + dbUser.Text + ":" + dbPass.Text);
                debug.write("Successfully wrote PgAdmin III authentication configuration to '" + PGADMIN3_PASSWORD_DIR + "\\pgpass.conf'");
            }
            catch { }
            // Launch Portable Postgres 3
            try
            {
                Process.Start(baseDirectory + "\\Postgres\\pgsql\\bin\\pgAdmin3.exe");
                debug.write("Successfully launched PgAdmin III.");
            }
            catch (Exception ex)
            {
                error("Failed to launch PgAdmin III application - try reinstalling your Postgres installation or check your secrutiy settings!", ex);
            }
        }
        #endregion

        #region "Methods - Postgres"
        /// <summary>
        /// Intializes the database for the database server - this is invoked when wiping or first creating a Postgres
        /// database server.
        /// </summary>
        /// <returns></returns>
        bool initDatabase(NewInstallSettings nis)
        {
            // Create database
            debug.write("Creating database...");
            try
            {
                ProcessOutput po = new ProcessOutput(baseDirectory + "\\Postgres\\pgsql\\bin\\initdb.exe", "-D \"" + baseDirectory + "\\Postgres\\Database\"");
                po.start(ref debug, "initdb.exe");
                debug.write("Process for database initialisation launched successfully.");
                po.proc.WaitForExit();
                debug.write("Process for database initialisation exited.");
            }
            catch (Exception ex)
            {
                error("Failed to create database.", ex);
                return false;
            }
            // Launch server
            launchPostgresServer();
            // Attempt to connect to the server continously, with a time-out of 20s; this is to test when the server is up ready for user-role creation
            debug.write("Checking for server response...");
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int tickStart = Environment.TickCount;
            bool sConnected = false;
            while (Environment.TickCount - tickStart < 20000 && !sConnected)
            {
                try
                {
                    s.Connect("127.0.0.1", 5432);
                    sConnected = true;
                    s.Disconnect(true);
                    s = null;
                }
                catch { }
            }
            if (!sConnected)
            {
                debug.write("No response from server; aborting init phase.");
                error("Failed to connect to Postgrss server for root user-role creation, try wiping the database.");
                return false;
            }
            // Create root user
            debug.write("Creating root user...");
            try
            {
                Process p = new Process();
                p.StartInfo.WorkingDirectory = baseDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "createuser.exe";
                p.StartInfo.Arguments = "-s -d -r -e root";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                debug.write("Process for database user creation launched successfully.");
                p.WaitForExit();
                debug.write("Process for database user creation exited.");
            }
            catch (Exception ex)
            {
                error("Failed to create database user.", ex);
                return true; // The base files installed fine, we'll return true
            }
            // Now we'll try to connect to the server every 200 m/s, with a time-out of 20s for the database server to start
            debug.write("Attempting to connect to the Postgres server using Npgsql...");
            tickStart = Environment.TickCount;
            NpgsqlConnection conn = null;
            while (Environment.TickCount - tickStart < 20000 && (conn = getDbConnection("127.0.0.1", 5432, "root", string.Empty, "postgres")) == null)
            {
                Thread.Sleep(200);
            }
            if (conn == null)
            {
                debug.write("Failed to connect to Postgres server using Npgsql!");
                error("Failed to establish connection to database, to create a new user; please wipe the database and try again!");
                return true; // The base files installed fine, we'll return true
            }
            // Check if to create a new user
            if (nis.user != "root")
            {
                debug.write("Adding user '" + nis.user + "'...");
                try
                {
                    NpgsqlCommand comm = new NpgsqlCommand("CREATE USER \"" + nis.user + "\" WITH PASSWORD '" + nis.pass + "' CREATEDB", conn);
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    error("Failed to create database user '" + nis.user + "'.", ex);
                    return true; // The base files installed fine, we'll return true
                }
            }
            // Check if to create a database
            if (nis.database != "postgres")
            {
                debug.write("Adding database '" + nis.database + "'...");
                try
                {
                    NpgsqlCommand comm = new NpgsqlCommand("CREATE DATABASE \"" + nis.database + "\"", conn);
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    error("Failed to create database '" + nis.database + "'.", ex);
                    return true; // The base files installed fine, we'll return true
                }
            }
            debug.write("Initialization process completed successfully!");
            return true;
        }
        /// <summary>
        /// Wipes the Postgres installation.
        /// </summary>
        bool wipe()
        {
            // Kill any server processes
            killAllProcesses();
            // Delete files
            try
            {
                if (Directory.Exists(baseDirectory + "\\Postgres"))
                    Directory.Delete(baseDirectory + "\\Postgres", true);
                debug.write("Successfully wiped Postgres installation located at '" + baseDirectory + "\\Postgres'.");
                return true;
            }
            catch (Exception ex)
            {
                error("Failed to wipe Postgres directory - permission or file-lock issue?", ex);
                return false;
            }
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
                debug.write("Killed DB launcher.");
            }
            catch { }
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
            debug.write("Killing all postgres processes...");
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
                p.StartInfo.WorkingDirectory = baseDirectory + "\\Postgres\\pgsql\\bin";
                p.StartInfo.FileName = "pg_ctl.exe";
                p.StartInfo.Arguments = "start -D \"" + baseDirectory + "\\Postgres\\Database" + "\"";
                if (lsHide.Checked) p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                p.Start();
                debug.write("Successfully launched Postgres database server process.");
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
                error("Failed to launch Postgres database server.", ex);
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
            try
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
            catch (Exception ex)
            {
                error("Failed to update settings for key '" + key + "'.", ex);
            }
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
            try
            {
                // Build text
                StringBuilder sb = new StringBuilder();
                XmlWriter xw = XmlWriter.Create(sb);
                settings.WriteTo(xw);
                xw.Close();
                // Write to file
                File.WriteAllText(baseDirectory + "\\Settings.xml", sb.ToString());
                debug.write("Successfully wrote settings to '" + baseDirectory + "\\Settings.xml'.");
            }
            catch (Exception ex)
            {
                error("Failed to save settings to '" + baseDirectory + "\\Settings.xml' - permission or file-lock issue?", ex);
            }
        }
        // Event handlers for value changing
        private void pathSQL_TextChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_CLIENT_PATH, pathSQL.Text);
        }
        private void dbUser_TextChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_CLIENT_USER, dbUser.Text);
        }
        private void dbPass_TextChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_CLIENT_PASS, dbPass.Text);
        }
        private void dbDatabase_TextChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_CLIENT_DB, dbDatabase.Text);
        }
        /// <summary>
        /// Invoked when the checkbox control for "Hide server window" is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsHide_CheckedChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_HIDE_SERVER_WINDOW, lsHide.Checked ? "1" : "0");
        }
        /// <summary>
        /// Invoked when the checkbox control for "Automatically launch" is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsAutoLaunch_CheckedChanged(object sender, EventArgs e)
        {
            updateSettings(SETTINGS_AUTO_LAUNCH, lsAutoLaunch.Checked ? "1" : "0");
        }
        #endregion

        #region "Methods - Installation Process"
        public void threadInstallRun(NewInstallSettings nis)
        {
            Thread th = new Thread(
                delegate()
                {
                    threadInstall(this, nis);
                }
                );
            th.Start();
        }
        public static void threadInstall(Main form, NewInstallSettings nis)
        {
            // Extract zip file
            form.installUpdateProgress("Extracting server files...", 10);
            form.debug.write("Extracting server files...");
            try
            {
                ZipFile f = new ZipFile(form.baseDirectory + "\\Postgres.zip");
                f.ExtractAll(form.baseDirectory + "\\Postgres");
                f.Dispose();
            }
            catch (Exception ex)
            {
                form.error("Failed to decompress zip-file at '" + form.baseDirectory + "\\Postgres.zip'.", ex);
                form.installUpdateProgress("Failed to extract zip-file - possibly a corrupt download...", 0);
                form.threadInstallEnableDownloading();
                return;
            }
            // Delete zip file
            form.installUpdateProgress("Deleting zip file...", 20);
            form.debug.write("Deleting zip file...");
            try
            {
                File.Delete(form.baseDirectory + "\\Postgres.zip");
            }
            catch(Exception ex)
            {
                form.debug.write("Error occurred deleting '" + form.baseDirectory + "\\Postgres.zip' - " + ex.Message + " - " + ex.StackTrace);
                return;
            }
            // Initialize database
            form.installUpdateProgress("Initializing database structure...", 30);
            form.debug.write("Initializing database structure...");
            if (form.initDatabase(nis))
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
        public static void threadWipePostgres(Main form)
        {
            form.Invoke((MethodInvoker)delegate()
            {
                // Show download group but disable abort and download buttons
                form.controlsShowDownload();
                form.buttDownload.Enabled = false;
                form.buttDownloadAbort.Enabled = false;
                // Shutdown Postgres server
                form.stopPostgresServer();
            });
            // Execute the method
            if (form.wipe())
            {
                form.controlsShowDownload();
                form.controlsEnableDownload();
            }
            else
                form.controlsShowLaunch();
            form.Invoke((MethodInvoker)delegate()
            {
                // Enable the download and abort buttons
                form.buttDownload.Enabled = true;
                form.buttDownload.Visible = true;
                form.buttDownloadAbort.Enabled = true;
                form.comboBox1.Enabled = true;
            });

        }
        /// <summary>
        /// Wipes the database used by the Postgres server.
        /// </summary>
        /// <param name="o"></param>
        public static void threadWipeDb(Main form)
        {
            // Disable launch group
            form.controlsDisableLaunch();
            // Grab settings
            NewInstallSettings nis = getInstallSettings(form);
            if (nis == null)
            {
                form.controlsEnableLaunch();
                return; // Operation cancelled
            }
            // Kill any potential processes
            form.killAllProcesses();
            // Delete the database directory if it exists
            if (Directory.Exists(form.baseDirectory + "\\Postgres\\Database"))
            {
                form.debug.write("Deleting existing database directory...");
                try
                {
                    Directory.Delete(form.baseDirectory + "\\Postgres\\Database", true);
                    form.debug.write("Successfully wiped Postgres database files at '" + form.baseDirectory + "\\Postgres\\Database'.");
                }
                catch (Exception ex)
                {
                    form.error("Failed to delete database directory.", ex);
                    return;
                }
            }
            // Reinitialize the database
            form.initDatabase(nis);
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

        #region "Methods - Misc"
        /// <summary>
        /// Launches a URL in the user's default browser.
        /// </summary>
        /// <param name="url"></param>
        public static void launchURL(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch external browser for the following URL:\r\n" + url + "\r\n\r\nError:" + ex.Message + "\r\n\r\nStack-trace:\r\n" + ex.StackTrace,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Throws a simple error message-box.
        /// </summary>
        /// <param name="text"></param>
        public void error(string text)
        {
            MessageBox.Show(text, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Throws a simple error message-box with the exception displayed to the user and logged to the debug console.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        public void error(string text, Exception ex)
        {
            MessageBox.Show(text + "\r\n\r\nMessage for developers/support:\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            debug.write("Error occurred - " + ex.Message + " - " + ex.StackTrace);
        }
        /// <summary>
        /// Grabs settings for a new Postgres installation from the user.
        /// </summary>
        /// <returns></returns>
        public static NewInstallSettings getInstallSettings(Main m)
        {
            // Grab new settings
            InstallSettings d = new InstallSettings();
            d.ShowDialog();
            NewInstallSettings sett = new NewInstallSettings();
            // Check the user entered details
            if ((sett.database = d.Settings_Database) != null && (sett.user = d.Settings_User) != null && (sett.pass = d.Settings_Pass) != null)
            {
                d.Dispose();
                m.Invoke((MethodInvoker)delegate()
                {
                    // Update client launcher settings
                    m.dbDatabase.Text = sett.database;
                    m.updateSettings(SETTINGS_CLIENT_DB, sett.database);
                    m.dbUser.Text = sett.user;
                    m.updateSettings(SETTINGS_CLIENT_USER, sett.user);
                    m.dbPass.Text = sett.pass;
                    m.updateSettings(SETTINGS_CLIENT_PASS, sett.pass);
                });
                return sett;
            }
            else
            {
                d.Dispose();
                return null;
            }
        }
        /// <summary>
        /// Creates a Postgres database connection.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public NpgsqlConnection getDbConnection(string ip, int port, string username, string password, string database)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server=" + ip + ";Port=" + port.ToString() + ";User Id=" + username + ";Password=" + password + ";Database=" + database);
                conn.Open();
                return conn;
            }
            catch(Exception ex)
            {
                debug.write(ex.Message);
                return null; // Failed to connect to the database
            }
        }
        #endregion
    }
    /// <summary>
    /// Data structure used to store specified installation settings from the user.
    /// </summary>
    public class NewInstallSettings
    {
        public string database, user, pass;
    }
    public class ProcessOutput
    {
        public Process proc;
        public ProcessOutput(string filename, string args)
        {
            proc = new Process();
            proc.StartInfo.FileName = filename;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
        }
        public void start(ref Debugger debug, string msgTitle)
        {
            proc.Start();
            string line;
            while ((line = proc.StandardOutput.ReadLine()) != null)
                debug.writeNoTrace(msgTitle + ": " + line);
        }
    }
}