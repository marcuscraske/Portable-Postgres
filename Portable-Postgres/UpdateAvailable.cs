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

namespace Portable_Postgres
{
    public partial class UpdateAvailable : Form
    {
        #region "Constants"
        private const string changeLogURL = "https://raw.github.com/ubermeat/Portable-Postgres/master/README";
        #endregion

        #region "Variables"
        private string versionCurrent;
        private string versionNew;
        #endregion

        #region "Constructors"
        public UpdateAvailable(string currentVersion, string newVersion)
        {
            InitializeComponent();
            versionCurrent = currentVersion;
            versionNew = newVersion;
        }
        #endregion

        #region "Methods - Events"
        /// <summary>
        /// Invoked when the user clicks the update button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttUpdate_Click(object sender, EventArgs e)
        {
            // Launch the updater application
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Update.exe");
            // Exit
            Application.Exit();
        }
        /// <summary>
        /// Invoked when the user clicks the ignore button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttIgnore_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Sets the labels with the current and new version info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateAvailable_Shown(object sender, EventArgs e)
        {
            txtCurrVersion.Text = versionCurrent;
            txtNewVersion.Text = versionNew;
        }
        /// <summary>
        /// Launches the URL with a change-log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(changeLogURL);
        }
        #endregion
    }
}