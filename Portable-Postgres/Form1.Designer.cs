namespace Portable_Postgres
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttDownloadAbort = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttDownload = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lsWipe = new System.Windows.Forms.Button();
            this.lsHide = new System.Windows.Forms.CheckBox();
            this.lsRestart = new System.Windows.Forms.Button();
            this.lsStop = new System.Windows.Forms.Button();
            this.lsStart = new System.Windows.Forms.Button();
            this.dbDatabase = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dbPass = new System.Windows.Forms.TextBox();
            this.dbUser = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pathSQL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.dbBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.saveSettings = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.installationProgress = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.db1 = new System.Windows.Forms.Panel();
            this.db2 = new System.Windows.Forms.Panel();
            this.db3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.versionText = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.db1.SuspendLayout();
            this.db2.SuspendLayout();
            this.db3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttDownloadAbort
            // 
            this.buttDownloadAbort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttDownloadAbort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttDownloadAbort.FlatAppearance.BorderSize = 0;
            this.buttDownloadAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttDownloadAbort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttDownloadAbort.Location = new System.Drawing.Point(441, 82);
            this.buttDownloadAbort.Name = "buttDownloadAbort";
            this.buttDownloadAbort.Size = new System.Drawing.Size(75, 23);
            this.buttDownloadAbort.TabIndex = 6;
            this.buttDownloadAbort.Text = "Abort";
            this.buttDownloadAbort.UseVisualStyleBackColor = false;
            this.buttDownloadAbort.Visible = false;
            this.buttDownloadAbort.Click += new System.EventHandler(this.buttDownloadAbort_Click);
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(7, 87);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(24, 13);
            this.statusText.TabIndex = 5;
            this.statusText.Text = "Idle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "-- Select the first item for 32-bit Windows or the second item for 64-bit Windows" +
    "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "URL:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "http://get.enterprisedb.com/postgresql/postgresql-9.1.2-1-windows-binaries.zip",
            "http://get.enterprisedb.com/postgresql/postgresql-9.1.2-1-windows-x64-binaries.zi" +
                "p",
            "(enter your own)"});
            this.comboBox1.Location = new System.Drawing.Point(51, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(546, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // buttDownload
            // 
            this.buttDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttDownload.FlatAppearance.BorderSize = 0;
            this.buttDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttDownload.Location = new System.Drawing.Point(522, 82);
            this.buttDownload.Name = "buttDownload";
            this.buttDownload.Size = new System.Drawing.Size(75, 23);
            this.buttDownload.TabIndex = 1;
            this.buttDownload.Text = "Download";
            this.buttDownload.UseVisualStyleBackColor = false;
            this.buttDownload.Click += new System.EventHandler(this.buttDownload_Click_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(590, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // lsWipe
            // 
            this.lsWipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lsWipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsWipe.FlatAppearance.BorderSize = 0;
            this.lsWipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lsWipe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lsWipe.Location = new System.Drawing.Point(371, 3);
            this.lsWipe.Name = "lsWipe";
            this.lsWipe.Size = new System.Drawing.Size(101, 26);
            this.lsWipe.TabIndex = 4;
            this.lsWipe.Text = "Wipe Database";
            this.lsWipe.UseVisualStyleBackColor = false;
            this.lsWipe.Click += new System.EventHandler(this.lsWipe_Click);
            // 
            // lsHide
            // 
            this.lsHide.AutoSize = true;
            this.lsHide.Checked = true;
            this.lsHide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lsHide.Location = new System.Drawing.Point(246, 9);
            this.lsHide.Name = "lsHide";
            this.lsHide.Size = new System.Drawing.Size(119, 17);
            this.lsHide.TabIndex = 3;
            this.lsHide.Text = "Hide server window";
            this.lsHide.UseVisualStyleBackColor = true;
            // 
            // lsRestart
            // 
            this.lsRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lsRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsRestart.FlatAppearance.BorderSize = 0;
            this.lsRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lsRestart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lsRestart.Location = new System.Drawing.Point(165, 3);
            this.lsRestart.Name = "lsRestart";
            this.lsRestart.Size = new System.Drawing.Size(75, 26);
            this.lsRestart.TabIndex = 2;
            this.lsRestart.Text = "Restart";
            this.lsRestart.UseVisualStyleBackColor = false;
            this.lsRestart.Click += new System.EventHandler(this.lsRestart_Click);
            // 
            // lsStop
            // 
            this.lsStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lsStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsStop.FlatAppearance.BorderSize = 0;
            this.lsStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lsStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lsStop.Location = new System.Drawing.Point(84, 3);
            this.lsStop.Name = "lsStop";
            this.lsStop.Size = new System.Drawing.Size(75, 26);
            this.lsStop.TabIndex = 1;
            this.lsStop.Text = "Stop";
            this.lsStop.UseVisualStyleBackColor = false;
            this.lsStop.Visible = false;
            this.lsStop.Click += new System.EventHandler(this.lsStop_Click);
            // 
            // lsStart
            // 
            this.lsStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lsStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsStart.FlatAppearance.BorderSize = 0;
            this.lsStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lsStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lsStart.Location = new System.Drawing.Point(3, 3);
            this.lsStart.Name = "lsStart";
            this.lsStart.Size = new System.Drawing.Size(75, 26);
            this.lsStart.TabIndex = 0;
            this.lsStart.Text = "Start";
            this.lsStart.UseVisualStyleBackColor = false;
            this.lsStart.Click += new System.EventHandler(this.lsStart_Click);
            // 
            // dbDatabase
            // 
            this.dbDatabase.Location = new System.Drawing.Point(445, 52);
            this.dbDatabase.Name = "dbDatabase";
            this.dbDatabase.Size = new System.Drawing.Size(149, 20);
            this.dbDatabase.TabIndex = 10;
            this.dbDatabase.Text = "postgres";
            this.dbDatabase.TextChanged += new System.EventHandler(this.dbDatabase_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(383, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Database:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Pass:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "User:";
            // 
            // dbPass
            // 
            this.dbPass.Location = new System.Drawing.Point(231, 52);
            this.dbPass.Name = "dbPass";
            this.dbPass.Size = new System.Drawing.Size(147, 20);
            this.dbPass.TabIndex = 6;
            this.dbPass.TextChanged += new System.EventHandler(this.dbPass_TextChanged);
            // 
            // dbUser
            // 
            this.dbUser.Location = new System.Drawing.Point(51, 52);
            this.dbUser.Name = "dbUser";
            this.dbUser.Size = new System.Drawing.Size(135, 20);
            this.dbUser.TabIndex = 5;
            this.dbUser.Text = "User";
            this.dbUser.TextChanged += new System.EventHandler(this.dbUser_TextChanged);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.Location = new System.Drawing.Point(4, 78);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(590, 26);
            this.button5.TabIndex = 4;
            this.button5.Text = "Launch Client";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Location = new System.Drawing.Point(519, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pathSQL
            // 
            this.pathSQL.Location = new System.Drawing.Point(7, 26);
            this.pathSQL.Name = "pathSQL";
            this.pathSQL.Size = new System.Drawing.Size(506, 20);
            this.pathSQL.TabIndex = 1;
            this.pathSQL.TextChanged += new System.EventHandler(this.pathSQL_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the path containing your SQL files:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.versionText);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.db3);
            this.splitContainer1.Panel1.Controls.Add(this.db2);
            this.splitContainer1.Panel1.Controls.Add(this.db1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Gray;
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Size = new System.Drawing.Size(786, 379);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 8;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.linkLabel1.Location = new System.Drawing.Point(0, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.linkLabel1.Size = new System.Drawing.Size(110, 17);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Contribute via Github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(520, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label2.Size = new System.Drawing.Size(266, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Author: limpygnome (limpygnome@gmail.com)\r\nCreative Commons Attribution-ShareAlik" +
    "e 3.0 unported";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dbBrowse
            // 
            this.dbBrowse.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // saveSettings
            // 
            this.saveSettings.Interval = 3000;
            this.saveSettings.Tick += new System.EventHandler(this.saveSettings_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(478, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 26);
            this.button1.TabIndex = 5;
            this.button1.Text = "Wipe Postgres";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // installationProgress
            // 
            this.installationProgress.Location = new System.Drawing.Point(299, 82);
            this.installationProgress.Maximum = 30;
            this.installationProgress.Name = "installationProgress";
            this.installationProgress.Size = new System.Drawing.Size(136, 23);
            this.installationProgress.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(537, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "If the client disappears after launch, try wiping the database a few times by cli" +
    "cking the \"Wipe Database\" button.";
            // 
            // db1
            // 
            this.db1.BackColor = System.Drawing.Color.Transparent;
            this.db1.Controls.Add(this.installationProgress);
            this.db1.Controls.Add(this.label3);
            this.db1.Controls.Add(this.buttDownloadAbort);
            this.db1.Controls.Add(this.progressBar1);
            this.db1.Controls.Add(this.statusText);
            this.db1.Controls.Add(this.buttDownload);
            this.db1.Controls.Add(this.label4);
            this.db1.Controls.Add(this.comboBox1);
            this.db1.ForeColor = System.Drawing.Color.White;
            this.db1.Location = new System.Drawing.Point(12, 28);
            this.db1.Name = "db1";
            this.db1.Size = new System.Drawing.Size(606, 113);
            this.db1.TabIndex = 4;
            // 
            // db2
            // 
            this.db2.BackColor = System.Drawing.Color.Transparent;
            this.db2.Controls.Add(this.button1);
            this.db2.Controls.Add(this.lsStart);
            this.db2.Controls.Add(this.lsWipe);
            this.db2.Controls.Add(this.lsStop);
            this.db2.Controls.Add(this.lsHide);
            this.db2.Controls.Add(this.lsRestart);
            this.db2.ForeColor = System.Drawing.Color.White;
            this.db2.Location = new System.Drawing.Point(3, 163);
            this.db2.Name = "db2";
            this.db2.Size = new System.Drawing.Size(615, 40);
            this.db2.TabIndex = 5;
            // 
            // db3
            // 
            this.db3.BackColor = System.Drawing.Color.Transparent;
            this.db3.Controls.Add(this.label8);
            this.db3.Controls.Add(this.label1);
            this.db3.Controls.Add(this.dbDatabase);
            this.db3.Controls.Add(this.pathSQL);
            this.db3.Controls.Add(this.label7);
            this.db3.Controls.Add(this.button4);
            this.db3.Controls.Add(this.label6);
            this.db3.Controls.Add(this.button5);
            this.db3.Controls.Add(this.label5);
            this.db3.Controls.Add(this.dbUser);
            this.db3.Controls.Add(this.dbPass);
            this.db3.ForeColor = System.Drawing.Color.White;
            this.db3.Location = new System.Drawing.Point(3, 209);
            this.db3.Name = "db3";
            this.db3.Size = new System.Drawing.Size(615, 134);
            this.db3.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "1. Download Postgres";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(3, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(262, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "2. Launch Postgres Server and Client";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Portable_Postgres.Properties.Resources.PostgreSQL_logo1;
            this.pictureBox1.Location = new System.Drawing.Point(624, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(636, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Portable Postgres";
            // 
            // versionText
            // 
            this.versionText.AutoSize = true;
            this.versionText.BackColor = System.Drawing.Color.Transparent;
            this.versionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionText.ForeColor = System.Drawing.Color.White;
            this.versionText.Location = new System.Drawing.Point(679, 192);
            this.versionText.Name = "versionText";
            this.versionText.Size = new System.Drawing.Size(42, 16);
            this.versionText.TabIndex = 11;
            this.versionText.Text = "v0.0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 379);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Portable Postgres - UberMeat.co.uk FOSS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.db1.ResumeLayout(false);
            this.db1.PerformLayout();
            this.db2.ResumeLayout(false);
            this.db2.PerformLayout();
            this.db3.ResumeLayout(false);
            this.db3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lsRestart;
        private System.Windows.Forms.Button lsStop;
        private System.Windows.Forms.Button lsStart;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox pathSQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttDownload;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Button buttDownloadAbort;
        private System.Windows.Forms.CheckBox lsHide;
        private System.Windows.Forms.Button lsWipe;
        private System.Windows.Forms.TextBox dbDatabase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dbPass;
        private System.Windows.Forms.TextBox dbUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog dbBrowse;
        private System.Windows.Forms.Timer saveSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar installationProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel db3;
        private System.Windows.Forms.Panel db2;
        private System.Windows.Forms.Panel db1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label versionText;
    }
}

