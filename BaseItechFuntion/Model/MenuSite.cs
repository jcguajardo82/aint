using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseItechFuntion.Model
{
    internal class MenuSite
    {
        public string label { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }

        public List<item>? items { get; set; }

    }


    internal class item
    {
        public string label { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }
    }

}
