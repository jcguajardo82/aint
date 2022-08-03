using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseItechFuntion.Model
{
    public class UserModel 
    {

        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public string fechaCreacion { get; set; } = string.Empty;
        public string autor { get; set; }
        public string modificadoPor { get; set; }
        public string fechaModificacion { get; set; } = string.Empty;
        public string usuario { get; set; }
        public string rol { get; set; }
        //  [idUsuario]
        //,[nombre]
        //,[activo]
        //,[fechaCreacion]
        //,[autor]
        //,[modificadoPor]
        //,[fechaModificacion]
        //,[usuario]
        //,[rol]
    }

    public class UserRolModel
    {
        public string nombreRol { get; set; }
        public string idRol { get; set; }
    }

    public class UserSimpleModelReq
    {
        public int idUsuario { get; set; }
    }
}