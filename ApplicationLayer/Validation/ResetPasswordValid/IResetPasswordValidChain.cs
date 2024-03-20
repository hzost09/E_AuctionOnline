using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.ResetPasswordValid
{
    internal interface IResetPasswordValidChain
    {
        Task<(bool, string)> validateSignUpChain(ResetPassWordModel model);
    }
}
