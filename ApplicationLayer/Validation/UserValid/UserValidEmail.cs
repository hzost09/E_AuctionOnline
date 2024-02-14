using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.UserValid
{
    internal class UserValidEmail : IUserValidChain
    {
        public Task<(bool, string)> validateChain(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
