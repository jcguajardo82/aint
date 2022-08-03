using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseItechFuntion.Model
{
    public class MenuModel
    {
        public int menuId { get; set; }
        public string descripcion { get; set; }
        public string descripcionCorta { get; set; }
        public int padreId { get; set; }
        public int posicion { get; set; }

        public string icono { get; set; }
        public bool habilitado { get; set; }
        public string url { get; set; }
        public bool esMenu { get; set; }
        public string target { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
    }

    public class SimpleMenuReq
    {
        public int idMenu { get; set; }
    }
}