using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.ResetPasswordValid
{
    internal class PassWordValid : IResetPasswordValidChain
    {
        public async Task<(bool, string)> validateSignUpChain(ResetPassWordModel model)
        {
            if (model.PasswordReset == null || model.ConfirmPassWord == null)
            {
                throw new ValidationException("All password input must be fill");
            }
            if (!IsValidPassword(model.PasswordReset))
            {
                throw new ValidationException("at least 8 character 1 upper 1 number");
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

    internal class EmailValid : IResetPasswordValidChain
    {
        public async Task<(bool, string)> validateSignUpChain(ResetPassWordModel model)
        {
            if (model.Email == null)
            {
                throw new ValidationException("Email input need to fill");
            }
            if (!IsValidEmail(model.Email))
            {
                throw new ValidationException("Invalid Email");
            }
            return (true,"");
        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }

    internal class TokenEmail : IResetPasswordValidChain
    {
        public async Task<(bool, string)> validateSignUpChain(ResetPassWordModel model)
        {
            if (model.EmailToken == null)
            {
                throw new ValidationException("cant Reconize User");
            }
            return (true, "");
        }
    }
}
