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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttDownloadAbort = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttDownload = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lsWipe = new System.Windows.Forms.Button();
            this.lsHide = new System.Windows.Forms.CheckBox();
            this.lsRestart = new System.Windows.Forms.Button();
            this.lsStop = new System.Windows.Forms.Button();
            this.lsStart = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttDownloadAbort);
            this.groupBox1.Controls.Add(this.statusText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.buttDownload);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Download Postgres";
            // 
            // buttDownloadAbort
            // 
            this.buttDownloadAbort.Location = new System.Drawing.Point(331, 92);
            this.buttDownloadAbort.Name = "buttDownloadAbort";
            this.buttDownloadAbort.Size = new System.Drawing.Size(75, 23);
            this.buttDownloadAbort.TabIndex = 6;
            this.buttDownloadAbort.Text = "Abort";
            this.buttDownloadAbort.UseVisualStyleBackColor = true;
            this.buttDownloadAbort.Visible = false;
            this.buttDownloadAbort.Click += new System.EventHandler(this.buttDownloadAbort_Click);
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(6, 92);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(24, 13);
            this.statusText.TabIndex = 5;
            this.statusText.Text = "Idle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "-- Select the first item for 32-bit Windows or the second item for 64-bit Windows" +
    "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 22);
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
            this.comboBox1.Location = new System.Drawing.Point(50, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(437, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // buttDownload
            // 
            this.buttDownload.Location = new System.Drawing.Point(412, 92);
            this.buttDownload.Name = "buttDownload";
            this.buttDownload.Size = new System.Drawing.Size(75, 23);
            this.buttDownload.TabIndex = 1;
            this.buttDownload.Text = "Download";
            this.buttDownload.UseVisualStyleBackColor = true;
            this.buttDownload.Click += new System.EventHandler(this.buttDownload_Click_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 63);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(481, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lsWipe);
            this.groupBox3.Controls.Add(this.lsHide);
            this.groupBox3.Controls.Add(this.lsRestart);
            this.groupBox3.Controls.Add(this.lsStop);
            this.groupBox3.Controls.Add(this.lsStart);
            this.groupBox3.Location = new System.Drawing.Point(3, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 61);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2. Launch Server";
            // 
            // lsWipe
            // 
            this.lsWipe.Location = new System.Drawing.Point(386, 19);
            this.lsWipe.Name = "lsWipe";
            this.lsWipe.Size = new System.Drawing.Size(101, 26);
            this.lsWipe.TabIndex = 4;
            this.lsWipe.Text = "Wipe Database";
            this.lsWipe.UseVisualStyleBackColor = true;
            this.lsWipe.Click += new System.EventHandler(this.lsWipe_Click);
            // 
            // lsHide
            // 
            this.lsHide.AutoSize = true;
            this.lsHide.Checked = true;
            this.lsHide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lsHide.Location = new System.Drawing.Point(261, 25);
            this.lsHide.Name = "lsHide";
            this.lsHide.Size = new System.Drawing.Size(119, 17);
            this.lsHide.TabIndex = 3;
            this.lsHide.Text = "Hide server window";
            this.lsHide.UseVisualStyleBackColor = true;
            // 
            // lsRestart
            // 
            this.lsRestart.Location = new System.Drawing.Point(180, 19);
            this.lsRestart.Name = "lsRestart";
            this.lsRestart.Size = new System.Drawing.Size(75, 26);
            this.lsRestart.TabIndex = 2;
            this.lsRestart.Text = "Restart";
            this.lsRestart.UseVisualStyleBackColor = true;
            this.lsRestart.Click += new System.EventHandler(this.lsRestart_Click);
            // 
            // lsStop
            // 
            this.lsStop.Location = new System.Drawing.Point(99, 19);
            this.lsStop.Name = "lsStop";
            this.lsStop.Size = new System.Drawing.Size(75, 26);
            this.lsStop.TabIndex = 1;
            this.lsStop.Text = "Stop";
            this.lsStop.UseVisualStyleBackColor = true;
            this.lsStop.Visible = false;
            this.lsStop.Click += new System.EventHandler(this.lsStop_Click);
            // 
            // lsStart
            // 
            this.lsStart.Location = new System.Drawing.Point(18, 19);
            this.lsStart.Name = "lsStart";
            this.lsStart.Size = new System.Drawing.Size(75, 26);
            this.lsStart.TabIndex = 0;
            this.lsStart.Text = "Start";
            this.lsStart.UseVisualStyleBackColor = true;
            this.lsStart.Click += new System.EventHandler(this.lsStart_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.dbDatabase);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.dbPass);
            this.groupBox4.Controls.Add(this.dbUser);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.pathSQL);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(3, 202);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(493, 162);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3. Launch Client";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 113);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(481, 32);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "If the client disappears after launch, try wiping the database a few times by cli" +
    "cking the \"Wipe Database\" button.";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dbDatabase
            // 
            this.dbDatabase.Location = new System.Drawing.Point(366, 58);
            this.dbDatabase.Name = "dbDatabase";
            this.dbDatabase.Size = new System.Drawing.Size(121, 20);
            this.dbDatabase.TabIndex = 10;
            this.dbDatabase.Text = "postgres";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(304, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Database:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Pass:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "User:";
            // 
            // dbPass
            // 
            this.dbPass.Location = new System.Drawing.Point(198, 58);
            this.dbPass.Name = "dbPass";
            this.dbPass.Size = new System.Drawing.Size(100, 20);
            this.dbPass.TabIndex = 6;
            // 
            // dbUser
            // 
            this.dbUser.Location = new System.Drawing.Point(53, 58);
            this.dbUser.Name = "dbUser";
            this.dbUser.Size = new System.Drawing.Size(100, 20);
            this.dbUser.TabIndex = 5;
            this.dbUser.Text = "User";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 84);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(481, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Launch Client";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(412, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pathSQL
            // 
            this.pathSQL.Location = new System.Drawing.Point(9, 32);
            this.pathSQL.Name = "pathSQL";
            this.pathSQL.Size = new System.Drawing.Size(397, 20);
            this.pathSQL.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(502, 384);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 8;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel1.Location = new System.Drawing.Point(0, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(2);
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
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(236, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(266, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Author: limpygnome (limpygnome@gmail.com)\r\nCreative Commons Attribution-ShareAlik" +
    "e 3.0 unported";
            // 
            // dbBrowse
            // 
            this.dbBrowse.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 384);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Portable Postgres - UberMeat.co.uk FOSS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button lsRestart;
        private System.Windows.Forms.Button lsStop;
        private System.Windows.Forms.Button lsStart;
        private System.Windows.Forms.GroupBox groupBox4;
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
        private System.Windows.Forms.TextBox textBox1;
    }
}

