using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseItechFuntion.Model
{
    public class RolModel
    {
        public int idRol { get; set; }
        public string nombreRol { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public bool activo { get; set; } = false;

    }

    public class MenuRolConfig : MenuModel
    {
        public int seleccionado { get; set; }
    }

    public class SetMenuRol
    {
        public int idRol { get; set; }

        public bool activo { get; set; }

        public int idMenu { get; set; }
    }
}