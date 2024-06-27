using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using ApplicationLayer.Validation.LoginValid;
using DomainLayer.Core;
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
      
        public AuthService(IUnitOfWork I)
        {
            _Iu = I;           

        }
        public async Task<(User,string)> login(LoginModel model)
        {
            try
            {            
                var findUser = await _Iu.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                if (findUser == null)
                {
                    return (null, "User not Found");

                }
                if (findUser.EmailConfirm == false)
                {
                    return (null,"Account not verify yet");
                }

                return (findUser, "Success login");
                
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
                var findemail = await _Iu.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (findemail != null)
                {
                    if (findemail.EmailConfirm == false)
                    {
                        return (1, "Your Account is not verify yet, please go to your email to verify by click on the link");
                    }
                }
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

        public async Task<(int,string)> confrimVerify(verifymodel model)
        {      
            var findverifymodel = await _Iu.Repository<VerifyEmail>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (findverifymodel.VerifyToken != model.verifyToken && findverifymodel.EndDate < DateTime.Now)
            {
                return(-1,"Invalid Token or Expire Token");
            }
            var finduser = await _Iu.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == model.Email);
            finduser.EmailConfirm = true;
            _Iu.Repository<User>().UpDate(finduser);
            await _Iu.SaveChangesAsync();
            return (1,"success");
        }
    }
}
