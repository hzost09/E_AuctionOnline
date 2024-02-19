using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.UserValid
{
    public interface IUserValidChain
    {
        Task<(bool, string)> validateUserChain(UserModel model);
    }
}
