using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseItechFuntion.Model
{
    internal class MenuRolTreeModel
    {
        public string label { get; set; }
        public string data { get; set; }
        public string expandedIcon { get; set; }
        public string collapsedIcon { get; set; }
        public string key { get; set; }

        public List<item> children { get; set; }

        public class item
        {
            public string label { get; set; }
            public string data { get; set; }
            public string expandedIcon { get; set; }
            public string collapsedIcon { get; set; }

            public string key { get; set; }

        }
    }
}
