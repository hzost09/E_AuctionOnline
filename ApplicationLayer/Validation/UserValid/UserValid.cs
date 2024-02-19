using ApplicationLayer.DTO;
using ApplicationLayer.Validation.RegisterValid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.UserValid
{
    public class UserValid
    {
        private readonly List<IUserValidChain> _valid = new List<IUserValidChain>();

        public UserValid AddValidator(IUserValidChain ilogchain)
        {
            _valid.Add(ilogchain);
            return this;
        }

        public async Task ActionValid(UserModel model)
        {
            foreach (var valid in _valid)
            {
                await valid.validateUserChain(model);
            }
        }
    }
}
