using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.LoginValid
{
    public class LoginValidate
    {
        private readonly List<ILoginChain> _valid = new List<ILoginChain>();

        public LoginValidate AddValidator(ILoginChain ilogchain)
        {
            _valid.Add( ilogchain );
            return this;
        }

        public async Task ActionValid(LoginModel model)
        {
            foreach (var valid in _valid)
            {
                 await valid.validateChain(model);
            }
        }
    }
}
