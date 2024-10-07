using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.User
{
    public class TwoFactorAuthenticationModel
    {
        public string UserName { get; set; }
        public string Code { get; set; }
        public string? Token { get; set; }
        public string? QRCodeUrl { get; set; }
    }
    public class TwoFactorAuthenticationResponseModel
    {
        public string? Msg {  get; set; }
    }
}
