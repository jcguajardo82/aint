using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using BaseItechFuntion.Model;
using BaseItechFuntion.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace BaseItechFuntion.Fn
{
    public static class Rol
    {
        [FunctionName("GetRoles")]
        public static async Task<IActionResult> GetRoles(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetRoles/{activo}")] HttpRequest req,
           ILogger log, bool activo)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.roles_sUP(activo);

                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = _result

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }

        [FunctionName("GetRol")]
        public static async Task<IActionResult> GetRol(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetRol/{idRol}")] HttpRequest req,
           ILogger log, int idRol)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.rolesByIdRol_sUP(idRol);

                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = _result

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }


        [FunctionName("AddRol")]
        public static async Task<IActionResult> AddRol(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddRol")] RolModel rol, HttpRequest req,
            ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.roles_iUP(rol);

                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }

        [FunctionName("UpdRol")]
        public static async Task<IActionResult> UpdRol(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdRol")] RolModel rol, HttpRequest req,
           ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.roles_uUP(rol);

                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }


        [FunctionName("GetMenuRol")]
        public static async Task<IActionResult> GetMenuRol(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenuRol/{idRol}")] HttpRequest req,
           ILogger log, int idRol)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.MenuRolConfig_sUp(idRol);


                var p = _result.Where(x => x.padreId == 0).ToList();

                List<MenuRolTreeModel> lst = new List<MenuRolTreeModel>();

                foreach (var pitem in p)
                {
                    var mrt = new MenuRolTreeModel();

                    mrt.label = pitem.descripcion + " - " + pitem.descripcionCorta;

                    mrt.expandedIcon = pitem.icono;
                    mrt.collapsedIcon = pitem.icono;
                    mrt.key = pitem.menuId.ToString();
                    mrt.data = pitem.seleccionado.ToString();

                    mrt.children = new List<MenuRolTreeModel.item>();

                    var h = _result.Where(z => z.padreId == pitem.menuId).ToList();

                    foreach (var hitem in h)
                    {
                        mrt.children.Add(
                        new MenuRolTreeModel.item
                        {
                            label = hitem.descripcion + " - " + hitem.descripcionCorta,

                            expandedIcon = hitem.icono,
                            collapsedIcon = hitem.icono,
                            key = hitem.menuId.ToString(),
                            data = hitem.seleccionado.ToString()

                        }
                        );
                    }



                    lst.Add(mrt);

                }




                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = lst

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }

        [FunctionName("SetMenuRol")]
        public static async Task<IActionResult> SetMenuRol(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetMenuRol")] SetMenuRol menuR, HttpRequest req,
           ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.MenuRolConfig_iUp(menuR);

                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }



        [FunctionName("GetMenuRolFuse")]
        public static async Task<IActionResult> GetMenuRolFuse(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenuRolFuse/{idRol}")] HttpRequest req,
           ILogger log, int idRol)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.MenuRolConfig_sUp(idRol);


                var p = _result.Where(x => x.padreId == 0).ToList();

                List<MenuRolTreeFuseModel> lst = new List<MenuRolTreeFuseModel>();

                foreach (var pitem in p)
                {
                    var mrt = new MenuRolTreeFuseModel();

                    mrt.name = pitem.descripcion + " - " + pitem.descripcionCorta;

                    //mrt.expandedIcon = pitem.icono;
                    //mrt.collapsedIcon = pitem.icono;
                    mrt.id = pitem.menuId;
                    mrt.selected = Convert.ToBoolean(pitem.seleccionado);

                    mrt.children = new List<MenuRolTreeFuseModel>();
                    mrt.parentId = pitem.padreId;
                    mrt.indeterminate = false;

                    var h = _result.Where(z => z.padreId == pitem.menuId).ToList();

                    foreach (var hitem in h)
                    {
                        mrt.children.Add(
                        new MenuRolTreeFuseModel
                        {
                            name = hitem.descripcion + " - " + hitem.descripcionCorta,

                            //expandedIcon = hitem.icono,
                            //collapsedIcon = hitem.icono,
                            id = hitem.menuId,
                            selected = Convert.ToBoolean(hitem.seleccionado),
                            parentId=hitem.padreId,
                            indeterminate=false
                        }
                        );
                    }



                    lst.Add(mrt);

                }




                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = lst

                })).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }
        }
    }
}
