using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Account
{
    public class GetTwoFactorAuthenticationUserModel
    {
        public bool rememberMe { set; get; }
        public string?returnUrl { set; get; }
    }
}
