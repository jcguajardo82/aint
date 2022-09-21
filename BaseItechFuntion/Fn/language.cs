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


        [FunctionName("GetLenguajes")]
        public static async Task<IActionResult> GetLenguajes(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetLenguajes")] HttpRequest req,
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

                var _result = objDal.Lenguaje_sUP();

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

        [FunctionName("GetLenguajesById")]
        public static async Task<IActionResult> GetLenguajesById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetLenguajesById/{id}")]HttpRequest req,
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

                var _result = objDal.LenguajesById_sUp(id);

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

        [FunctionName("AddLenguaje")]
        public static async Task<IActionResult> AddLenguaje(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AddLenguaje")] LenguajeModel lenguaje, HttpRequest req,
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

                objDal.Lenguajes_iUp(lenguaje);

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

        [FunctionName("UpdLenguaje")]
        public static async Task<IActionResult> UpdLenguaje(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdLenguaje")] LenguajeModel lenguaje, HttpRequest req,
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

                objDal.Lenguajes_uUp(lenguaje);

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

        [FunctionName("DelLenguaje")]
        public static async Task<IActionResult> DelLenguaje(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DelLenguaje/{id}")]  HttpRequest req,
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

                objDal.Lenguajes_dUp(id);

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

        [FunctionName("GetCategorias")]
        public static async Task<IActionResult> GetCategorias(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCategorias")] HttpRequest req,
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

                var _result = objDal.EtiquetaModulos_sUp();

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

        [FunctionName("EtiquetaValorByIdLenguaje")]
        public static async Task<IActionResult> EtiquetaValorByIdLenguaje(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "EtiquetaValorByIdLenguaje")] ComboModel Filtro, HttpRequest req,
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

                var _result = objDal.EtiquetaValorByIdLenguaje(int.Parse(Filtro.Id),Filtro.Value);

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

        [FunctionName("UpdEtiquetaVal")]
        public static async Task<IActionResult> UpdEtiquetaVal(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdEtiquetaVal")] ComboModel etiqueta, HttpRequest req,
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

                objDal.EtiquetaValor_uUp(etiqueta);

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
