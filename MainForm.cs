using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using CraigsCreations.com.X10;
using ModemRebooter.Properties;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using System.IO;

namespace ModemRebooter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _notifyIcon.Icon = Resources.Modem;

            UpdateStatus();
        }

        public string TrayIconStatusText
        {
            set { _notifyIcon.Text = value; }
        }

        private ModemStatus _modemStatus = ModemStatus.Online;
        private ModemStatus ModemStatus
        {
            get { return _modemStatus; }
            set
            {
                OnStatusChanging(_modemStatus, value);
                _modemStatus = value;
            }
        }

        /// <summary>
        /// Called when the value of IsConnected is changing.  If the values are different,
        /// log the change and set the icon and tooltip for the system tray.  If the new
        /// status is Offline, reboot the modem.
        /// </summary>
        private void OnStatusChanging(ModemStatus oldStatus, ModemStatus newStatus)
        {
            if (oldStatus != newStatus)
            {
                if (newStatus == ModemStatus.Online)
                {
                    Program.LogMessage("Modem is now online.");
                    _notifyIcon.Icon = Resources.Modem;
                }
                else
                {
                    Program.LogMessage("Modem is now offline.");
                    Program.LogMessage("Rebooting modem...");
                    RebootModem();
                    _notifyIcon.Icon = Resources.BadModem;
                }
            }
        }

        /// <summary>
        /// Update the status on each timer tick.
        /// </summary>
        private void _timer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        /// <summary>
        /// Get the latest status from the modem.
        /// </summary>
        private void UpdateStatus()
        {
            ModemStatus = Modem.GetStatus();
        }

        /// <summary>
        /// Reboot the modem.
        /// </summary>
        private void RebootModem()
        {
            _timer.Enabled = false;
            Modem.Reboot();
            _timer.Enabled = true;
        }

        /// <summary>
        /// Close the app when Exit is selected from the tray icon's menu.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
