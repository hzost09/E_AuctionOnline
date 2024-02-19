using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using ApplicationLayer.Validation.LoginValid;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class AuthService: IAuthService
    {
        private readonly IUnitOfWork _Iu;
        private readonly IJwtService _j;
        public AuthService(IUnitOfWork I, IJwtService j)
        {
            _Iu = I;
            _j = j;

        }
        public async Task<(User,string)> login(LoginModel model)
        {
            try
            {            
                var findUser = await _Iu.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (findUser == null)
                {
                    return (null, "User not Found");
                }
                else
                {              
                    return (findUser, "Success login");
                }
            }
            catch (ValidationException ex)
            {
                return (null, ex.Message);
            }
            
        }

        public async Task<(int,string)> Register(SignUpModel model)
        {
            try
            {
                var newUser = new User();
                newUser.Email = model.Email;
                newUser.Name = model.UserName;
                newUser.Password = model.Password;
                var addUser = await _Iu.Repository<User>().Create(newUser);
                await _Iu.SaveChangesAsync();
                return (1,"Register Success");
            }
            catch (ValidationException ex)
            {
                await _Iu.RollBackChangesAsync();
                return (-1, ex.Message);
            }
        }
    }
}
