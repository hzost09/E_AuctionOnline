using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class ResetPassWordModel
    {
       
        public string Email { get; set; }

        public string EmailToken { get; set; }
        
        public string PasswordReset { get; set; }
        
        public string ConfirmPassWord { get; set; }
    }
}
