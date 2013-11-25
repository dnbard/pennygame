using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyGameLibrary.Parser
{
    public class Game
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string InternalLink { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string SalePrice { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Store { get; set; }
    }
}
