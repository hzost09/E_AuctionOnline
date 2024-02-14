using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.RegisterValid
{
    public interface ISignUpChain
    {
        Task<(bool, string)> validateSignUpChain(SignUpModel model);
       
    }
}
