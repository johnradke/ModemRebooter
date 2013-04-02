using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;

namespace ModemRebooter
{
    static class Program
    {
        const string LOG_FILE = "C:\\rebooter.log";

        private static MainForm _mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var otherOpenProcesses = Process.GetProcessesByName(Assembly.GetExecutingAssembly().GetName().Name).Where(p => p.Id != Process.GetCurrentProcess().Id);
                if (otherOpenProcesses.Count() > 0)
                {
                    MessageBox.Show("Only one instance of this application is allowed at a time.", "Modem Rebooter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.Delete(LOG_FILE);
                LogMessage("ModemRebooter Started.");
                _mainForm = new MainForm();

                Application.Run(_mainForm);
            }
            catch (Exception e)
            {
                OnApplicationException(e);
                throw;
            }
        }

        static void OnApplicationException(Exception exception)
        {
            LogMessage(string.Format("Exception occurred!  Text:\r\n\r\n{0}", exception.ToString()));
        }

        public static void LogMessage(string message)
        {
            File.AppendAllText(LOG_FILE, string.Format("{0} - {1}\r\n", DateTime.Now, message));
            if (_mainForm != null)
            {
                _mainForm.TrayIconStatusText = message;
            }
        }
    }
}
