using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using BaseItechFuntion.Helpers;
using BaseItechFuntion.Model;

namespace BaseItechFuntion.Fn
{
    public static class User
    {
        [FunctionName("GetUsers")]
        public static async Task<IActionResult> GetUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetUsers")] HttpRequest req, ILogger log)
        {
            try
            {
                string jsonCancelShipmentRequest = await new StreamReader(req.Body).ReadToEndAsync();
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.usuarios_sUP();

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

        [FunctionName("AddUser")]
        public static async Task<IActionResult> AddUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddUser")] HttpRequest req, ILogger log)
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

                UserModel user = JsonConvert.DeserializeObject<UserModel>(jsonUserModelRequest);
                #endregion

                DAL.DAL objDal = new DAL.DAL();

                objDal.Usuarios_iUP(user);

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

        [FunctionName("UpdUser")]
        public static async Task<IActionResult> UpdUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdUser")] HttpRequest req,ILogger log)
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

                UserModel user = JsonConvert.DeserializeObject<UserModel>(jsonUserModelRequest);
                #endregion

                DAL.DAL objDal = new DAL.DAL();

                objDal.Usuarios_uUP(user);

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

        [FunctionName("GetUser")]
        public static async Task<IActionResult> GetUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetUser")] HttpRequest req, ILogger log)
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

                UserSimpleModelReq user = JsonConvert.DeserializeObject<UserSimpleModelReq>(jsonUserModelRequest);
                #endregion

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.UsuariosById_sUP(user.idUsuario);

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
    }
}
