/*
 * Creative Commons Attribution-ShareAlike 3.0 unported
 * ***************************************************************
 * Author:  limpygnome
 * E-mail:  limpygnome@gmail.com
 * Site:    ubermeat.co.uk
 * ***************************************************************
 * Credit to:
 * -- none
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Portable_Postgres
{
    public partial class NewInstall : Form
    {
        #region "Constructor"
        public NewInstall()
        {
            InitializeComponent();
        }
        #endregion

        #region "Methods - Events"
        /// <summary>
        /// Invoked when the user presses the button "No".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttNo_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Invoked when the user presses the button "Okay".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttOkay_Click(object sender, EventArgs e)
        {
            try
            {
                // Credit: http://stackoverflow.com/questions/4897655/create-shortcut-on-desktop-c-sharp
                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Portable Postgres.url"))
                {
                    string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine("URL=file:///" + app);
                    writer.WriteLine("IconIndex=0");
                    string icon = app.Replace('\\', '/');
                    writer.WriteLine("IconFile=" + icon);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create desktop shortcut:\n\n" + ex.Message + "\n\nApologies, please let us know about this error!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
        private void NewInstall_Load(object sender, EventArgs e)
        {
            BringToFront();
        }
        #endregion
    }
}
