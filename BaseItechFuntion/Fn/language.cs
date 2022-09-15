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
    public static class language
    {
        [FunctionName("GetLabels")]
        public static async Task<IActionResult> GetUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetLabels/{languaje}")] HttpRequest req,
        ILogger log, string languaje)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                var _result = objDal.EtiquetaValor_sUp(languaje);

                return await Task.FromResult(new OkObjectResult(_result)).ConfigureAwait(false);
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

        [FunctionName("AddEtiqueta")]
        public static async Task<IActionResult> AddEtiqueta(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddEtiqueta")] EtiquetasModel etiqueta, HttpRequest req,
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

                objDal.Etiquetas_iUp(etiqueta);

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

        [FunctionName("UpdEtiqueta")]
        public static async Task<IActionResult> UpdEtiqueta(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdEtiqueta")] EtiquetasModel etiqueta, HttpRequest req,
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

                objDal.Etiquetas_iUp(etiqueta);

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

        [FunctionName("DelEtiqueta")]
        public static async Task<IActionResult> DelEtiqueta(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "DelEtiqueta/{id}")]  HttpRequest req,
        ILogger log,int id)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

                objDal.Etiquetas_dUp(id);

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

        [FunctionName("GetEtiquetas")]
        public static async Task<IActionResult> GetEtiquetas(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetEtiquetas/{id}")] HttpRequest req,
        ILogger log, int id)
        {
            try
            {
                ValidateJWT auth = new ValidateJWT(req);

                if (!auth.IsValid)
                {
                    return new UnauthorizedResult(); // No authentication info.
                }

                DAL.DAL objDal = new DAL.DAL();

               var _result= objDal.EtiquetasById_sUp(id);

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
