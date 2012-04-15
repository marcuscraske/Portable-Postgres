namespace Portable_Postgres
{
    partial class DetectionWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectionWindow));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLabelNew = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelProcessRunning = new System.Windows.Forms.Panel();
            this.buttTerminate = new System.Windows.Forms.Button();
            this.txtCurrVersion = new System.Windows.Forms.Label();
            this.txtLabelCurrVersion = new System.Windows.Forms.Label();
            this.panelServiceRunning = new System.Windows.Forms.Panel();
            this.buttStopService = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelPort = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttIgnore = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelProcessRunning.SuspendLayout();
            this.panelServiceRunning.SuspendLayout();
            this.panelPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 3);
            this.panel2.TabIndex = 10;
            // 
            // txtLabelNew
            // 
            this.txtLabelNew.AutoSize = true;
            this.txtLabelNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelNew.Location = new System.Drawing.Point(12, 41);
            this.txtLabelNew.Name = "txtLabelNew";
            this.txtLabelNew.Size = new System.Drawing.Size(392, 32);
            this.txtLabelNew.TabIndex = 11;
            this.txtLabelNew.Text = "Possible conflicts have been found, which may not allow Portable\r\nPostgres to ope" +
    "rate or install correctly:";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 35);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Runtime Issues Detected";
            // 
            // panelProcessRunning
            // 
            this.panelProcessRunning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProcessRunning.Controls.Add(this.buttTerminate);
            this.panelProcessRunning.Controls.Add(this.txtCurrVersion);
            this.panelProcessRunning.Controls.Add(this.txtLabelCurrVersion);
            this.panelProcessRunning.Location = new System.Drawing.Point(7, 82);
            this.panelProcessRunning.Name = "panelProcessRunning";
            this.panelProcessRunning.Size = new System.Drawing.Size(420, 71);
            this.panelProcessRunning.TabIndex = 17;
            // 
            // buttTerminate
            // 
            this.buttTerminate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttTerminate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttTerminate.FlatAppearance.BorderSize = 0;
            this.buttTerminate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttTerminate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttTerminate.Location = new System.Drawing.Point(334, 28);
            this.buttTerminate.Name = "buttTerminate";
            this.buttTerminate.Size = new System.Drawing.Size(80, 26);
            this.buttTerminate.TabIndex = 20;
            this.buttTerminate.Text = "Terminate";
            this.buttTerminate.UseVisualStyleBackColor = false;
            this.buttTerminate.Click += new System.EventHandler(this.buttTerminate_Click);
            // 
            // txtCurrVersion
            // 
            this.txtCurrVersion.AutoSize = true;
            this.txtCurrVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrVersion.Location = new System.Drawing.Point(3, 22);
            this.txtCurrVersion.Name = "txtCurrVersion";
            this.txtCurrVersion.Size = new System.Drawing.Size(316, 32);
            this.txtCurrVersion.TabIndex = 19;
            this.txtCurrVersion.Text = "A process with the same name as a critical Postgres\r\nprocess is already running.";
            // 
            // txtLabelCurrVersion
            // 
            this.txtLabelCurrVersion.AutoSize = true;
            this.txtLabelCurrVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelCurrVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.txtLabelCurrVersion.Location = new System.Drawing.Point(3, 6);
            this.txtLabelCurrVersion.Name = "txtLabelCurrVersion";
            this.txtLabelCurrVersion.Size = new System.Drawing.Size(241, 16);
            this.txtLabelCurrVersion.TabIndex = 18;
            this.txtLabelCurrVersion.Text = "Postgres process already running";
            // 
            // panelServiceRunning
            // 
            this.panelServiceRunning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelServiceRunning.Controls.Add(this.buttStopService);
            this.panelServiceRunning.Controls.Add(this.label2);
            this.panelServiceRunning.Controls.Add(this.label3);
            this.panelServiceRunning.Location = new System.Drawing.Point(7, 159);
            this.panelServiceRunning.Name = "panelServiceRunning";
            this.panelServiceRunning.Size = new System.Drawing.Size(420, 92);
            this.panelServiceRunning.TabIndex = 18;
            // 
            // buttStopService
            // 
            this.buttStopService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttStopService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttStopService.FlatAppearance.BorderSize = 0;
            this.buttStopService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttStopService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttStopService.Location = new System.Drawing.Point(334, 28);
            this.buttStopService.Name = "buttStopService";
            this.buttStopService.Size = new System.Drawing.Size(80, 26);
            this.buttStopService.TabIndex = 20;
            this.buttStopService.Text = "Stop service";
            this.buttStopService.UseVisualStyleBackColor = false;
            this.buttStopService.Click += new System.EventHandler(this.buttStopService_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 64);
            this.label2.TabIndex = 19;
            this.label2.Text = "A service with the word \"Postgres\" has been found;\r\nthis may be a possible Postgr" +
    "es database running\r\nin the background, which may conflict with\r\nPortable Postgr" +
    "es.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Postgres service running";
            // 
            // panelPort
            // 
            this.panelPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPort.Controls.Add(this.label4);
            this.panelPort.Controls.Add(this.label5);
            this.panelPort.Location = new System.Drawing.Point(7, 257);
            this.panelPort.Name = "panelPort";
            this.panelPort.Size = new System.Drawing.Size(420, 92);
            this.panelPort.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(414, 48);
            this.label4.TabIndex = 19;
            this.label4.Text = "The default port used by Postgres is already in-use!\r\nNo resolution can be provid" +
    "ed by Portable Postgres; you will need to\r\nfind the conflicting program and stop" +
    "/uninstall it!";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Port 5432 already in-use";
            // 
            // buttIgnore
            // 
            this.buttIgnore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttIgnore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttIgnore.FlatAppearance.BorderSize = 0;
            this.buttIgnore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttIgnore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttIgnore.Location = new System.Drawing.Point(326, 355);
            this.buttIgnore.Name = "buttIgnore";
            this.buttIgnore.Size = new System.Drawing.Size(101, 26);
            this.buttIgnore.TabIndex = 16;
            this.buttIgnore.Text = "Continue";
            this.buttIgnore.UseVisualStyleBackColor = false;
            this.buttIgnore.Click += new System.EventHandler(this.buttIgnore_Click);
            // 
            // DetectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 387);
            this.Controls.Add(this.panelPort);
            this.Controls.Add(this.panelServiceRunning);
            this.Controls.Add(this.panelProcessRunning);
            this.Controls.Add(this.buttIgnore);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtLabelNew);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DetectionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Runtime Issues Detected - Portable Postgres - Ubermeat.co.uk FOSS";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DetectionWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProcessRunning.ResumeLayout(false);
            this.panelProcessRunning.PerformLayout();
            this.panelServiceRunning.ResumeLayout(false);
            this.panelServiceRunning.PerformLayout();
            this.panelPort.ResumeLayout(false);
            this.panelPort.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtLabelNew;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelProcessRunning;
        private System.Windows.Forms.Button buttTerminate;
        private System.Windows.Forms.Label txtCurrVersion;
        private System.Windows.Forms.Label txtLabelCurrVersion;
        private System.Windows.Forms.Panel panelServiceRunning;
        private System.Windows.Forms.Button buttStopService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttIgnore;
    }
}