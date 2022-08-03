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

namespace BaseItechFuntion.Fn
{
    public static class Rol
    {
        [FunctionName("AddRol")]
        public static async Task<IActionResult> AddRol(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddRol")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonRolModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                RolModel rol = JsonConvert.DeserializeObject<RolModel>(jsonRolModelRequest);
                #endregion

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
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdRol")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonRolModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                RolModel rol = JsonConvert.DeserializeObject<RolModel>(jsonRolModelRequest);
                #endregion

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
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetMenuRol")] HttpRequest req, ILogger log)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonRolModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                RolModel rol = JsonConvert.DeserializeObject<RolModel>(jsonRolModelRequest);
                #endregion

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.MenuRolConfig_sUp(rol.idRol);

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

        [FunctionName("SetMenuRol")]
        public static async Task<IActionResult> SetMenuRol(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetMenuRol")] HttpRequest req, ILogger log)
        {
            try
            {           
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                #region Map Request
                string jsonRolModelRequest = await new StreamReader(req.Body).ReadToEndAsync();

                SetMenuRol menuR = JsonConvert.DeserializeObject<SetMenuRol>(jsonRolModelRequest);
                #endregion

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
    }
}
