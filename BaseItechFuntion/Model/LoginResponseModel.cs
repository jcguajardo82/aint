using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseItechFuntion.Model
{
    public class LoginResponseModel
    {
        public string accessToken { get; set; }

        public UserResponse user { get; set; }

       
    }
    public class UserResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string status { get; set; }


    }

}
