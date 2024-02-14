using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.LoginValid
{
    public class LogInEmailValidate : ILoginChain
    {
        public async Task<(bool, string)> validateChain(LoginModel model)
        {
            if (model.Email == null)
            {
                throw new ValidationException( "Email is require");
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
    public class LogInPaswordValidate : ILoginChain
    {
        public async Task<(bool, string)> validateChain(LoginModel model)
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
}
