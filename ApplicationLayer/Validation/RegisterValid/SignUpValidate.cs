using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.RegisterValid
{
    public class SignUpValidate
    {
        private readonly List<ISignUpChain> _valid = new List<ISignUpChain>();

        public SignUpValidate AddValidator(ISignUpChain ilogchain)
        {
            _valid.Add( ilogchain );
            return this;
        }

        public async Task ActionValid(SignUpModel model)
        {
            foreach (var valid in _valid)
            {
                 await valid.validateSignUpChain(model);
            }
        }
    }
}
