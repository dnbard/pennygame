using System;
using PennyGameLibrary.Database;
using PennyGameLibrary.Parser;
using PennyGameLibrary.Utility;

namespace PennyGameWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Write("Ready to parse data");

            //ParserManager.AddUrl("steampowered.com");
            //ParserManager.DoWork();
            ParserManager.ParseSteam();
            
            
            
            Console.ReadLine();
        }
    }
}
