using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.User
{
    public class GetAuthenticatorTokenModel
    {
        public string UserName { get; set; }


        
    }
    public class GetAuthenticatorKeyResponseModel
    {
        public string? Token { get; set; }
        public string? QRCodeUrl { get; set; }
        
    }
}
