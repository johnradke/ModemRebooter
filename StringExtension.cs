using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModemRebooter
{
    public static class StringExtension
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
