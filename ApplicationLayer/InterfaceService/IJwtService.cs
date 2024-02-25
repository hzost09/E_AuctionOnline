using DomainLayer.Core;
using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    public interface IJwtService
    {
        Task<string> CreateToken(User user);
        Task<ReFreshToken> createRrefreshtoken(int id);
        Task<ReFreshToken> RefreshAccessToken(string token);
        string dataFormToken(string token);
        Task<VerifyEmail> createVerifytoken(string email);
    }
}
