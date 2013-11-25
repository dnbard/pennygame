using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyGameLibrary.Database
{
    class ServerCredentials
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Password { get; set; }

        public static ServerCredentials Instance;

        public ServerCredentials()
        {
            Instance = this;
        }
    }
}
