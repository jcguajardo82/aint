using BaseItechFuntion.Helpers;
using BaseItechFuntion.Model;

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
                   

                    Dictionary<string, object> claims = new Dictionary<string, object>
                        {
                            // JSON representation of the user Reference with ID and display name
                            { "username", credentials.username },
 
                            // TODO: Add other claims here as necessary; maybe from a user database
                            { "role", userInfo.Rol},

                            { "roleId", userInfo.RolId },

                            { "exp",new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeMilliseconds()}
                        };

                    GenerateJWTToken generateJWTToken = new();
                    string token = generateJWTToken.IssuingJWT(claims);
                    

                    UserResponse userI = new UserResponse();


                    userI.id = userInfo.RolId;
                    userI.name = userInfo.UserName;
                    userI.email = String.Empty;
                    userI.avatar = String.Empty;
                    userI.status = String.Empty;



                    return await Task.FromResult(new OkObjectResult(new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = new LoginResponseModel
                        {
                            accessToken = token,
                            user = userI,
                            rol = userInfo.Rol,
                            idRol=userInfo.Rol
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
