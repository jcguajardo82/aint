using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseItechFuntion.Model
{
    internal class MenuRolTreeFuseModel
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<MenuRolTreeFuseModel> children { get; set; }
        public bool selected { get; set; }
        public bool indeterminate { get; set; }

        public int parentId { get; set; }


    }
}
