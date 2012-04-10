namespace Portable_Postgres
{
    partial class NewInstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewInstall));
            this.buttNo = new System.Windows.Forms.Button();
            this.buttOkay = new System.Windows.Forms.Button();
            this.txtLabelNew = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttNo
            // 
            this.buttNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttNo.FlatAppearance.BorderSize = 0;
            this.buttNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttNo.Location = new System.Drawing.Point(7, 73);
            this.buttNo.Name = "buttNo";
            this.buttNo.Size = new System.Drawing.Size(82, 26);
            this.buttNo.TabIndex = 17;
            this.buttNo.Text = "No thanks";
            this.buttNo.UseVisualStyleBackColor = false;
            this.buttNo.Click += new System.EventHandler(this.buttNo_Click);
            // 
            // buttOkay
            // 
            this.buttOkay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttOkay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttOkay.FlatAppearance.BorderSize = 0;
            this.buttOkay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttOkay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttOkay.Location = new System.Drawing.Point(230, 73);
            this.buttOkay.Name = "buttOkay";
            this.buttOkay.Size = new System.Drawing.Size(101, 26);
            this.buttOkay.TabIndex = 16;
            this.buttOkay.Text = "Okay";
            this.buttOkay.UseVisualStyleBackColor = false;
            this.buttOkay.Click += new System.EventHandler(this.buttOkay_Click);
            // 
            // txtLabelNew
            // 
            this.txtLabelNew.AutoSize = true;
            this.txtLabelNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelNew.Location = new System.Drawing.Point(19, 46);
            this.txtLabelNew.Name = "txtLabelNew";
            this.txtLabelNew.Size = new System.Drawing.Size(303, 16);
            this.txtLabelNew.TabIndex = 11;
            this.txtLabelNew.Text = "Would you like to add a shortcut on your desktop?";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(343, 3);
            this.panel2.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 35);
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
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "New Installation";
            // 
            // NewInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(343, 110);
            this.Controls.Add(this.buttNo);
            this.Controls.Add(this.buttOkay);
            this.Controls.Add(this.txtLabelNew);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Installation - Portable Postgres - UberMeat.co.uk FOSS";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NewInstall_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttNo;
        private System.Windows.Forms.Button buttOkay;
        private System.Windows.Forms.Label txtLabelNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}