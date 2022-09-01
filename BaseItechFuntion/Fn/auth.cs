using BaseItechFuntion.Helpers;
using BaseItechFuntion.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BaseItechFuntion.Fn
{
    public static class auth
    {
        [FunctionName("auth")]
        public static async Task<IActionResult> SingIn(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] LoginModel credentials, HttpRequest req,
            ILogger log)
        {
            try
            {
                DAL.DAL objDal = new DAL.DAL();

                var userInfo = objDal.Autenticar_sUP(credentials);


                if (userInfo.UserName == null)
                {
                    //return await Task.FromResult(new UnauthorizedResult()).ConfigureAwait(false);
                    throw new Exception("Usuario Incorrecto");
                }
                else
                {

                    UserResponse userI = new UserResponse();


                    userI.id = userInfo.RolId;
                    userI.name = userInfo.UserName;
                    userI.email = userInfo.correo;
                    userI.avatar = userInfo.avatar;
                    userI.status = null ;


                    Dictionary<string, object> claims = new Dictionary<string, object>
                        {
                            // JSON representation of the user Reference with ID and display name
                            { "username", credentials.username },
 
                            // TODO: Add other claims here as necessary; maybe from a user database
                            { "role", userInfo.Rol},

                            { "roleId", userInfo.RolId },

                            { "exp",new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeMilliseconds()},

                            { "user",userI}
                        };

                    GenerateJWTToken generateJWTToken = new();
                    string token = generateJWTToken.IssuingJWT(claims);
                    





                    return await Task.FromResult(new OkObjectResult(new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = new LoginResponseModel
                        {
                            accessToken = token,
                            user = userI,
                            rol = userInfo.Rol,
                            idRol=userInfo.RolId
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
        }
    }
}
