using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    public interface IAuthService
    {
        Task<(User, string)> login(LoginModel model);
        Task<(int, string)> Register(SignUpModel model);
    }
}
