using BaseItechFuntion.Helpers;
using BaseItechFuntion.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseItechFuntion.DAL
{
    public class DAL
    {
        private readonly string connString;

        public DAL()
        {
            connString = Environment.GetEnvironmentVariable("MyConnectionString", EnvironmentVariableTarget.Process);

        }
        public UserInfoModel Autenticar_sUP(LoginModel Aut)
        {

            DataTable result = new DataTable();
            UserInfoModel userinfo = new UserInfoModel();


            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[Autenticar_sUP]";

                        //command.Parameters.AddWithValue("@Usuario", Aut.User);

                        command.Parameters.Add(new SqlParameter("Usuario", Aut.username) { SqlDbType = System.Data.SqlDbType.Char });
                        command.Parameters.Add(new SqlParameter("@pass", Aut.password) { SqlDbType = System.Data.SqlDbType.NVarChar });

                        result = new DataTable();
                        sda.Fill(result);
                    }
                }
            }

            foreach (DataRow item in result.Rows)
            {
                userinfo.UserName = item["nombre"].ToString();
                userinfo.Rol = item["nombreRol"].ToString();
                userinfo.RolId = item["rol"].ToString();
                userinfo.correo = item["correo"].ToString();
                userinfo.avatar = null;
            }

            return userinfo;


        }

        #region Usuarios
        public List<UserModel> usuarios_sUP()
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[usuarios_sUP]";

                        //command.Parameters.AddWithValue("@Usuario", Aut.User);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<UserModel>(result.Tables[0]);


        }
        public void Usuarios_iUP(UserModel user)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[usuarios_iUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;




                    sqlComm.Parameters.AddWithValue("@nombre", user.nombre);
                    sqlComm.Parameters.AddWithValue("@activo", user.activo);
                    sqlComm.Parameters.AddWithValue("@autor", user.autor);
                    sqlComm.Parameters.AddWithValue("@usuario", user.usuario);
                    sqlComm.Parameters.AddWithValue("@rol", user.rol);
                    sqlComm.Parameters.AddWithValue("@correo", user.correo);
                    sqlComm.Parameters.AddWithValue("@password", user.password);

                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }
        public void Usuarios_uUP(UserModel user)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[usuarios_uUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;




                    sqlComm.Parameters.AddWithValue("@nombre", user.nombre);
                    sqlComm.Parameters.AddWithValue("@idUsuario", user.idUsuario);
                    sqlComm.Parameters.AddWithValue("@activo", user.activo);
                    sqlComm.Parameters.AddWithValue("@autor", user.autor);
                    sqlComm.Parameters.AddWithValue("@usuario", user.usuario);
                    sqlComm.Parameters.AddWithValue("@rol", user.rol);
                    sqlComm.Parameters.AddWithValue("@correo", user.correo);
                    sqlComm.Parameters.AddWithValue("@password", user.password);

                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }
        public UserModel UsuariosById_sUP(int idUsuario)
        {
            DataSet result;
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[UsuariosById_sUP]";

                        command.Parameters.AddWithValue("@idUsuario", idUsuario);

                        //command.Parameters.Add(new SqlParameter("idUsuario", idUsuario) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }




            return DataTableToModel.ConvertTo<UserModel>(result.Tables[0]).FirstOrDefault();


        }
        #endregion

        #region Rol
        public void roles_iUP(RolModel rol)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[roles_iUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@nombreRol", rol.nombreRol);
                    sqlComm.Parameters.AddWithValue("@descripcion", rol.descripcion);
                    sqlComm.Parameters.AddWithValue("@activo", rol.activo);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void roles_uUP(RolModel rol)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[roles_uUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@idrol", rol.idRol);
                    sqlComm.Parameters.AddWithValue("@nombreRol", rol.nombreRol);
                    sqlComm.Parameters.AddWithValue("@descripcion", rol.descripcion);
                    sqlComm.Parameters.AddWithValue("@activo", rol.activo);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public List<MenuRolConfig> MenuRolConfig_sUp(int idRol)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[MenuRolConfig_sUp]";

                        command.Parameters.AddWithValue("@idRol", idRol);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<MenuRolConfig>(result.Tables[0]);


        }

        public List<RolModel> roles_sUP(bool activo = true)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[roles_sUP]";

                        command.Parameters.AddWithValue("@activo", activo);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<RolModel>(result.Tables[0]);


        }

        public RolModel rolesByIdRol_sUP(int IdRol)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[rolesByIdRol_sUP]";

                        command.Parameters.AddWithValue("@IdRol", IdRol);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<RolModel>(result.Tables[0]).FirstOrDefault();


        }


        public void MenuRolConfig_iUp(SetMenuRol menuR)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[MenuRolConfig_iUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@idRol", menuR.idRol);
                    sqlComm.Parameters.AddWithValue("@habilitado", menuR.activo);
                    sqlComm.Parameters.AddWithValue("@menuId", menuR.idMenu);




                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }
        //MenuRolConfig
        #endregion

        #region Menu
        public void Menu_iUP(MenuModel menu)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Menu_iUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@menuId", menu.menuId);
                    sqlComm.Parameters.AddWithValue("@descripcion", menu.descripcion);
                    sqlComm.Parameters.AddWithValue("@descripcionCorta", menu.descripcionCorta);
                    sqlComm.Parameters.AddWithValue("@padreId", menu.padreId);
                    sqlComm.Parameters.AddWithValue("@icono", menu.icono);
                    sqlComm.Parameters.AddWithValue("@controller", menu.controller);
                    sqlComm.Parameters.AddWithValue("@Action", menu.action);
                    sqlComm.Parameters.AddWithValue("@habilitado", menu.habilitado);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void Menu_uUP(MenuModel menu)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Menu_uUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@menuId", menu.menuId);
                    sqlComm.Parameters.AddWithValue("@descripcion", menu.descripcion);
                    sqlComm.Parameters.AddWithValue("@descripcionCorta", menu.descripcionCorta);
                    sqlComm.Parameters.AddWithValue("@icono", menu.icono);
                    sqlComm.Parameters.AddWithValue("@url", menu.url);

                    sqlComm.Parameters.AddWithValue("@habilitado", menu.habilitado);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public List<MenuModel> Menu_sUP()
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[Menu_sUP]";

                        //command.Parameters.AddWithValue("@Usuario", Aut.User);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<MenuModel>(result.Tables[0]);


        }

        public MenuModel MenuByIdMenu_sUP(int menuId)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[MenuByIdMenu_sUP]";

                        command.Parameters.AddWithValue("@menuId", menuId);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<MenuModel>(result.Tables[0]).FirstOrDefault();


        }
        //MenuByIdMenu_sUP

        public List<MenuModel> MenusRol_sUP(int idRol)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[MenusRol_sUP]";

                        command.Parameters.AddWithValue("@idRol", idRol);

                        //command.Parameters.Add(new SqlParameter("Usuario", Aut.Username) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }


            return DataTableToModel.ConvertTo<MenuModel>(result.Tables[0]);


        }


        public void Menu_dUP(int menuId)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Menu_dUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@menuId", menuId);

                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void MenuSub_dUP(int menuId)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[MenuSub_dUP]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@menuId", menuId);

                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        #endregion

        #region Lenguajes
        public Dictionary<string, string> EtiquetaValor_sUp(string lenguaje)
        {
            //List<object> etiquetas = new List<object>();
            Dictionary<string, string> etiquetas = new Dictionary<string, string>();


            DataSet result;
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[EtiquetaValor_sUp]";

                        command.Parameters.AddWithValue("@lenguaje", lenguaje);

                        //command.Parameters.Add(new SqlParameter("idUsuario", idUsuario) { SqlDbType = System.Data.SqlDbType.Char });

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            foreach (DataRow item in result.Tables[0].Rows)
            {
                etiquetas.Add(item["label"].ToString(), item["valor"].ToString());
            }




            return etiquetas;


        }


        public void Etiquetas_iUp(EtiquetasModel etiqueta)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Etiquetas_iUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@label", etiqueta.label);
                    sqlComm.Parameters.AddWithValue("@modulo", etiqueta.modulo);
                    sqlComm.Parameters.AddWithValue("@descripcion", etiqueta.descripcion);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void Etiquetas_uUp(EtiquetasModel etiqueta)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Etiquetas_uUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@idLabel", etiqueta.idlabel);
                    sqlComm.Parameters.AddWithValue("@label", etiqueta.label);
                    sqlComm.Parameters.AddWithValue("@modulo", etiqueta.modulo);
                    sqlComm.Parameters.AddWithValue("@descripcion", etiqueta.descripcion);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void Etiquetas_dUp(int idLabel)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Etiquetas_dUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@idLabel", idLabel);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public List<EtiquetasModel> Etiquetas_sUp()
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[Etiquetas_sUp]";



                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<EtiquetasModel>(result.Tables[0]);

        }

        public List<EtiquetasModel> EtiquetasById_sUp(int idLabel)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[EtiquetasById_sUp]";

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idLabel", idLabel);

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<EtiquetasModel>(result.Tables[0]);
        }

        public List<LenguajeModel> Lenguaje_sUP()
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[Lenguaje_sUP]";



                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<LenguajeModel>(result.Tables[0]);

        }



        public void Lenguajes_iUp(LenguajeModel lenguaje)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Lenguajes_iUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@nombre", lenguaje.Nombre);
                    sqlComm.Parameters.AddWithValue("@codigo", lenguaje.Codigo);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void Lenguajes_uUp(LenguajeModel lenguaje)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Lenguajes_uUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@IdLenguaje", lenguaje.IdLenguaje);
                    sqlComm.Parameters.AddWithValue("@nombre", lenguaje.Nombre);
                    sqlComm.Parameters.AddWithValue("@codigo", lenguaje.Codigo);


                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public void Lenguajes_dUp(int IdLenguaje)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[Lenguajes_dUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@IdLenguaje", IdLenguaje);



                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }

        public LenguajeModel LenguajesById_sUp(int IdLenguaje)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[LenguajesById_sUp]";

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdLenguaje", IdLenguaje);

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<LenguajeModel>(result.Tables[0]).FirstOrDefault();
        }


        public List<ComboModel> EtiquetaModulos_sUp()
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[EtiquetaModulos_sUp]";



                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<ComboModel>(result.Tables[0]);

        }


        public List<EtiquetaValModel> EtiquetaValorByIdLenguaje(int IdLenguaje, string modulo)
        {

            DataSet result = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "[config].[EtiquetaValorByIdLenguaje_sUp]";

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdLenguaje", IdLenguaje);
                        command.Parameters.AddWithValue("@modulo", modulo);

                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }

            return DataTableToModel.ConvertTo<EtiquetaValModel>(result.Tables[0]);
        }

        public void EtiquetaValor_uUp(ComboModel etiqueta)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand sqlComm = new SqlCommand("[config].[EtiquetaValor_uUp]", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@idValue", etiqueta.Id);
                    sqlComm.Parameters.AddWithValue("@valor", etiqueta.Value);



                    con.Open();
                    sqlComm.ExecuteReader();

                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);


                }
            }

        }
        #endregion
    }
}
