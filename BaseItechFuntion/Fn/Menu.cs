using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using BaseItechFuntion.Helpers;
using BaseItechFuntion.Model;
using System.Linq;
using System.Collections.Generic;

namespace BaseItechFuntion.Fn
{
    public static class Menu
    {
        [FunctionName("GetMenus")]
        public static async Task<IActionResult> GetMenus(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenus")] HttpRequest req,
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

                var _result = objDal.Menu_sUP();

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

        [FunctionName("GetMenu")]
        public static async Task<IActionResult> GetMenu(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenu/{idMenu}")] HttpRequest req,
          ILogger log, int idMenu)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.MenuByIdMenu_sUP(idMenu);

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

        [FunctionName("AddMenu")]
        public static async Task<IActionResult> AddMenu(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddMenu")] MenuModel menu, HttpRequest req,
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

                objDal.Menu_iUP(menu);

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

        [FunctionName("UpdMenu")]
        public static async Task<IActionResult> UpdMenu(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdMenu")] MenuModel menu, HttpRequest req,
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

                objDal.Menu_uUP(menu);

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

        [FunctionName("GetMenusRol")]
        public static async Task<IActionResult> GetMenusRol(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenusRol")] HttpRequest req,
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

                var _result = objDal.MenusRol_sUP(int.Parse(auth.RoleId));

                var padres = _result.Where(x => x.padreId == 0).ToList();

                var menuLst = new List<MenuSite>();

                foreach (var p in padres)
                {


                    var mitem = new MenuSite();

                    mitem.icon = p.icono;
                    mitem.label = p.descripcionCorta;
                    mitem.routerLink = p.url;
                    mitem.items = new List<item>();
                    var h = _result.Where(z => z.padreId == p.menuId).ToList();


                    foreach (var item in h)
                    {


                        mitem.items.Add(
                            new item
                            {
                                icon = item.icono,
                                label = item.descripcionCorta,
                                routerLink = item.url
                            }
                            );
                    }
                    menuLst.Add(mitem);
                }


                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = menuLst
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

        [FunctionName("GetMenusPadre")]
        public static async Task<IActionResult> GetMenusPadre(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenusPadre")] HttpRequest req,
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

                var _result = objDal.Menu_sUP().Where(x => x.padreId == 0).ToList();

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

        [FunctionName("GetMenusHijos")]
        public static async Task<IActionResult> GetMenusHijos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenusHijos/{padreId}")] HttpRequest req,
            ILogger log, int padreId)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.Menu_sUP().Where(x => x.padreId == padreId).ToList();

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

        [FunctionName("DelMenu")]
        public static async Task<IActionResult> DelMenu(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DelMenu/{menuId}")] HttpRequest req,
        ILogger log, int menuId)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.Menu_dUP(menuId);

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

        [FunctionName("DelMenuSub")]
        public static async Task<IActionResult> DelMenuSub(
          [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DelMenuSub/{menuId}")] HttpRequest req,
          ILogger log, int menuId)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.MenuSub_dUP(menuId);

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


        [FunctionName("GetMenusRolF")]
        public static async Task<IActionResult> GetMenusRolF(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMenusRolF")] HttpRequest req,
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

                var _result = objDal.MenusRol_sUP(int.Parse(auth.RoleId));

                var padres = _result.Where(x => x.padreId == 0).ToList();

                var menuLst = new MenuFuseModel();
                menuLst.Default = new List<FuseNavigationItem>();
                menuLst.Compact = new List<FuseNavigationItem>();
                menuLst.Futuristic = new List<FuseNavigationItem>();
                menuLst.Horizontal = new List<FuseNavigationItem>();


                foreach (var p in padres)
                {


                    var mitem = new FuseNavigationItem();

                    mitem.icon = p.icono;
                    mitem.title = p.descripcionCorta;
                    mitem.link = p.url;
                    mitem.type = "basic";
                    mitem.id = p.menuId.ToString();
                    //mitem.items = new List<item>();
                    var h = _result.Where(z => z.padreId == p.menuId).ToList();


                    //foreach (var item in h)
                    //{


                    //    mitem.items.Add(
                    //        new item
                    //        {
                    //            icon = item.icono,
                    //            label = item.descripcionCorta,
                    //            routerLink = item.url
                    //        }
                    //        );
                    //}
                    menuLst.Default.Add(mitem);
                    menuLst.Compact.Add(mitem);
                    menuLst.Futuristic.Add(mitem);
                    menuLst.Horizontal.Add(mitem);
                }


                return await Task.FromResult(new OkObjectResult(new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = menuLst
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
