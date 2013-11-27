using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyGameLibrary.Utility
{
    internal abstract class LogOutput
    {
        protected string FormatDate()
        {
            var date = DateTime.Now;
            return date.ToString("G", CultureInfo.InvariantCulture);
        }

        public abstract void Write(string data);
        public abstract void WriteError(string data);
    }

}
