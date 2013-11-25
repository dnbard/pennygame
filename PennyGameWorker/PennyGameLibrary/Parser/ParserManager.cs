using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using PennyGameLibrary.Utility;

namespace PennyGameLibrary.Parser
{
    public class ParserManager
    {
        private static readonly ParserManager _instance = new ParserManager();
        private static ParserManager Instance { get { return _instance; } }
        
        private readonly Queue<string> _urls = new Queue<string>();

        public static void AddUrl(string url)
        {
            Instance._urls.Enqueue(url);
            Logger.Write("Parser. Added " + url);
        }

        private static string GetUrl()
        {
            if (Instance._urls.Count > 0)
            {
                var url = Instance._urls.Dequeue();
                if (!url.Contains("http"))
                    url = "http://" + url;
                return url;
            }
            return null;
        }

        private ParserManager()
        {
            
        }

        public static void DoWork()
        {
            var url = GetUrl();
            
            if (url == null)
            {
                Logger.Write("Parser queue is empty");
            } else
            {

                var doc = CQ.CreateFromUrl(url);
            }
        }

        public static void ParseSteam()
        {
            var steamParser = new SteamParser();
        }
    }
}
