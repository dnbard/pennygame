using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyGameLibrary.Utility
{
    public class Logger
    {
        List<LogOutput> channels = new List<LogOutput>();
        private static Logger Instance = new Logger();

        private Logger()
        {
            channels.Add(new ConsoleOutput());
        }

        public static void Write(string data)
        {
            Instance.channels.ForEach(ch => ch.Write(data));
        }

        public static void Write(string format, params object[] p)
        {
            var str = string.Format(format, p);
            Write(str);
        }
    }
}
