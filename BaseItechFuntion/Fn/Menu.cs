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

namespace BaseItechFuntion.Fn
{
    public static class Menu
    {
        [FunctionName("GetMenus")]
        public static async Task<IActionResult> GetMenus(
          [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetMenus")] HttpRequest req, ILogger log)
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
          [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetMenu")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonMenuModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                SimpleMenuReq menu = JsonConvert.DeserializeObject<SimpleMenuReq>(jsonMenuModelRequest);
                #endregion

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.MenuByIdMenu_sUP(menu.idMenu);

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddMenu")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonUserModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                MenuModel menu = JsonConvert.DeserializeObject<MenuModel>(jsonUserModelRequest);
                #endregion

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdMenu")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonUserModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                MenuModel menu = JsonConvert.DeserializeObject<MenuModel>(jsonUserModelRequest);
                #endregion

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
    }
}
