using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using PennyGameLibrary.Utility;

namespace PennyGameLibrary.Parser
{
    abstract class BaseParser
    {
        protected abstract void Parse();
        public string Url { get; protected set; }
        protected CQ Document { get; set; }

        public BaseParser(string url)
        {
            Url = url;

            try
            {
                Document = CQ.CreateFromUrl(url);
                Parse();
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
            }
        }

        protected BaseParser()
        {
        }
    }
}
