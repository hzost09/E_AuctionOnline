using ApplicationLayer.DTO;
using ApplicationLayer.Validation.RegisterValid;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.UserValid
{
    public class UserValidEmail : IUserValidChain
    {
        private readonly IUnitOfWork _u;
        public UserValidEmail(IUnitOfWork u)
        {
             _u = u;    
        }
        public async Task<(bool, string)> validateChain(UserModel model)
        {
            var findEmail = await _u.Repository<User>().GetUserByEmail<User>(model.Email);
            if (findEmail != null)
            {
                throw new ValidationException("Email is Exit pls chose other Email");

            }
            if (model.Email == null)
            {
                throw new ValidationException("Email is require");
            }
            if (!IsValidEmail(model.Email))
            {
                throw new ValidationException("Invalid Email");
            }
            return (true, "");
        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
    public class NameValidate : IUserValidChain
    {
        public async Task<(bool, string)> validateChain(UserModel model)
        {
            if (model.Name == "" || model.Name == null)
            {
                throw new ValidationException("UserNames required");
            }
            if (model.Name.Length < 2)
            {
                throw new ValidationException("UserNames have at least 3 character");
            }
            return (true, "");
        }
    }

}
