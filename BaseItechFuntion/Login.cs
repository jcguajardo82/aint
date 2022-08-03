using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using BaseItechFuntion.Model;
using BaseItechFuntion.Helpers;

namespace BaseItechFuntion
{
    public static class Login
    {

        [FunctionName("Login")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] LoginModel login, HttpRequest req,
            ILogger log)
        {
            try
            {
                //string jsonCancelShipmentRequest = await new StreamReader(req.Body).ReadToEndAsync();

                DAL.DAL objDal = new DAL.DAL();

                var userInfo = objDal.Autenticar_sUP(login);


                if (userInfo.UserName == null)
                {
                    return await Task.FromResult(new UnauthorizedResult()).ConfigureAwait(false);
                }
                else
                {
                    Dictionary<string, object> claims = new Dictionary<string, object>
                        {
                            // JSON representation of the user Reference with ID and display name
                            { "username", login.username },
 
                            // TODO: Add other claims here as necessary; maybe from a user database
                            { "role", userInfo.Rol},
                            { "name", userInfo.UserName },
                        };

                    GenerateJWTToken generateJWTToken = new();
                    string token = generateJWTToken.IssuingJWT(claims);

                    return await Task.FromResult(new OkObjectResult(new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = new
                        {
                            token = "bar"
                        }

                        })).ConfigureAwait(false);
                }

            }
            catch (Exception ex)
            {

                return await Task.FromResult(new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                })).ConfigureAwait(false);
            }


            //return await Task.FromResult(new BadRequestObjectResult(new Response
            //{
            //    IsSuccess = true,
            //    Message = "Ok"
            //})).ConfigureAwait(false);
        }
    }
}
