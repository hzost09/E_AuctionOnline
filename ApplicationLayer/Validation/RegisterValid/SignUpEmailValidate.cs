using ApplicationLayer.DTO;
using ApplicationLayer.Validation.RegisterValid;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.RegisterValid
{
    public class SignUpEmailValidate : ISignUpChain
    {
        private readonly IUnitOfWork _u;
        public SignUpEmailValidate(IUnitOfWork u)
        {
            _u = u;
        }
        public async Task<(bool, string)> validateSignUpChain(SignUpModel model)
        {
            if (model.Email == null)
            {
                throw new ValidationException( "Email is require");
            }
            if (!IsValidEmail(model.Email))
            {
                throw new ValidationException("Invalid Email");       
            }
            var findEmailExit = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (findEmailExit != null && findEmailExit.EmailConfirm == true)
            {
                throw new ValidationException("Email exited change to other Email");
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
    public class SignUpPaswordValidate : ISignUpChain
    {
        public async Task<(bool, string)> validateSignUpChain(SignUpModel model)
        {
            if (model.Password == null)
            {
                throw new ValidationException("Password required");
            }
            if (!IsValidPassword(model.Password))
            {
                throw new ValidationException( "Invalid Password: at least 8 char 1 Upper 1 number");
            }
            return (true, "");
        }


        public bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

    }
    public class SignUpUserNameValidate : ISignUpChain
    {
        public async Task<(bool, string)> validateSignUpChain(SignUpModel model)
        {
            if (model.UserName == "" || model.UserName == null)
            {
                throw new ValidationException("UserNames required");
            }
            if (model.UserName.Length < 2)
            {
                throw new ValidationException("UserNames have at least 3 character");
            }
            return (true, "");
        }
    }
}
