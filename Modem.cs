using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraigsCreations.com.X10;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace ModemRebooter
{
    public static class Modem
    {
        private static object _lockObject = new object();

        /// <summary>
        /// Triggers the X10 receiver that the modem is plugged into
        /// to turn off, wait 10 seconds, and turn back on.
        /// </summary>
        public static void Reboot()
        {
            lock (_lockObject)
            {
                Program.LogMessage("Connecting to X11 Controller.");
                using (var controller = new X10CM11aController(HouseCode.J, "COM1"))
                {
                    var appliance = new X10Application(controller, 11);
                    
                    Program.LogMessage("Turning receiver OFF.");
                    appliance.Off();

                    appliance.

                    Thread.Sleep(10000);

                    Program.LogMessage("Turning receiver ON.");
                    appliance.On();
                }
            }
        }

        /// <summary>
        /// Determine if the modem is online
        /// </summary>
        /// <returns></returns>
        private static bool ModemIsOnline()
        {
            try
            {
                TcpClient tcpClient = new TcpClient("google.com", 80);
                tcpClient.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines if the modem is operational.
        /// </summary>
        public static ModemStatus GetStatus()
        {
            var retries = 1;
            for (int p = 0; p < retries; p++)
            {
                if (ModemIsOnline())
                {
                    return ModemStatus.Online;
                }
            }
            return ModemStatus.Offline;
        }
    }
}
