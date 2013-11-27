using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyGameLibrary.Utility
{
    sealed class ConsoleOutput: LogOutput
    {
        public override void Write(string data)
        {
            Console.WriteLine("{0} # {1}",FormatDate(), data);
        }

        public override void WriteError(string data)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;           
            Console.WriteLine("{0} # {1}", FormatDate(), data);
            Console.ResetColor();
        }
    }
}
