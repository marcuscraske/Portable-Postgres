namespace Portable_Postgres
{
    partial class UpdateAvailable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAvailable));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLabelNew = new System.Windows.Forms.Label();
            this.txtLabelCurrVersion = new System.Windows.Forms.Label();
            this.txtLabelNewVersion = new System.Windows.Forms.Label();
            this.txtCurrVersion = new System.Windows.Forms.Label();
            this.txtNewVersion = new System.Windows.Forms.Label();
            this.buttUpdate = new System.Windows.Forms.Button();
            this.buttIgnore = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 35);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Update Available";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(470, 3);
            this.panel2.TabIndex = 1;
            // 
            // txtLabelNew
            // 
            this.txtLabelNew.AutoSize = true;
            this.txtLabelNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelNew.Location = new System.Drawing.Point(85, 41);
            this.txtLabelNew.Name = "txtLabelNew";
            this.txtLabelNew.Size = new System.Drawing.Size(299, 16);
            this.txtLabelNew.TabIndex = 2;
            this.txtLabelNew.Text = "A new update for Portable Postgres is available...\r\n";
            // 
            // txtLabelCurrVersion
            // 
            this.txtLabelCurrVersion.AutoSize = true;
            this.txtLabelCurrVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelCurrVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.txtLabelCurrVersion.Location = new System.Drawing.Point(12, 75);
            this.txtLabelCurrVersion.Name = "txtLabelCurrVersion";
            this.txtLabelCurrVersion.Size = new System.Drawing.Size(116, 16);
            this.txtLabelCurrVersion.TabIndex = 3;
            this.txtLabelCurrVersion.Text = "Current version:";
            // 
            // txtLabelNewVersion
            // 
            this.txtLabelNewVersion.AutoSize = true;
            this.txtLabelNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelNewVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.txtLabelNewVersion.Location = new System.Drawing.Point(12, 127);
            this.txtLabelNewVersion.Name = "txtLabelNewVersion";
            this.txtLabelNewVersion.Size = new System.Drawing.Size(97, 16);
            this.txtLabelNewVersion.TabIndex = 4;
            this.txtLabelNewVersion.Text = "New version:";
            // 
            // txtCurrVersion
            // 
            this.txtCurrVersion.AutoSize = true;
            this.txtCurrVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrVersion.Location = new System.Drawing.Point(12, 91);
            this.txtCurrVersion.Name = "txtCurrVersion";
            this.txtCurrVersion.Size = new System.Drawing.Size(63, 16);
            this.txtCurrVersion.TabIndex = 5;
            this.txtCurrVersion.Text = "Unknown";
            // 
            // txtNewVersion
            // 
            this.txtNewVersion.AutoSize = true;
            this.txtNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewVersion.Location = new System.Drawing.Point(12, 143);
            this.txtNewVersion.Name = "txtNewVersion";
            this.txtNewVersion.Size = new System.Drawing.Size(63, 16);
            this.txtNewVersion.TabIndex = 6;
            this.txtNewVersion.Text = "Unknown";
            // 
            // buttUpdate
            // 
            this.buttUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttUpdate.FlatAppearance.BorderSize = 0;
            this.buttUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttUpdate.Location = new System.Drawing.Point(357, 145);
            this.buttUpdate.Name = "buttUpdate";
            this.buttUpdate.Size = new System.Drawing.Size(101, 26);
            this.buttUpdate.TabIndex = 7;
            this.buttUpdate.Text = "Update";
            this.buttUpdate.UseVisualStyleBackColor = false;
            this.buttUpdate.Click += new System.EventHandler(this.buttUpdate_Click);
            // 
            // buttIgnore
            // 
            this.buttIgnore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttIgnore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttIgnore.FlatAppearance.BorderSize = 0;
            this.buttIgnore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttIgnore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttIgnore.Location = new System.Drawing.Point(173, 145);
            this.buttIgnore.Name = "buttIgnore";
            this.buttIgnore.Size = new System.Drawing.Size(70, 26);
            this.buttIgnore.TabIndex = 8;
            this.buttIgnore.Text = "Ignore";
            this.buttIgnore.UseVisualStyleBackColor = false;
            this.buttIgnore.Click += new System.EventHandler(this.buttIgnore_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(249, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 26);
            this.button1.TabIndex = 9;
            this.button1.Text = "View Changelog";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UpdateAvailable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 183);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttIgnore);
            this.Controls.Add(this.buttUpdate);
            this.Controls.Add(this.txtNewVersion);
            this.Controls.Add(this.txtCurrVersion);
            this.Controls.Add(this.txtLabelNewVersion);
            this.Controls.Add(this.txtLabelCurrVersion);
            this.Controls.Add(this.txtLabelNew);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateAvailable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Available - Portable Postgres - UberMeat.co.uk FOSS";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.UpdateAvailable_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtLabelNew;
        private System.Windows.Forms.Label txtLabelCurrVersion;
        private System.Windows.Forms.Label txtLabelNewVersion;
        private System.Windows.Forms.Label txtCurrVersion;
        private System.Windows.Forms.Label txtNewVersion;
        private System.Windows.Forms.Button buttUpdate;
        private System.Windows.Forms.Button buttIgnore;
        private System.Windows.Forms.Button button1;
    }
}