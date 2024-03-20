using ApplicationLayer.InterfaceService;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class JWTservice:IJwtService
    {
        private readonly IUnitOfWork _u;
        private readonly IConfiguration _config;
        public JWTservice(IUnitOfWork u, IConfiguration config)
        {
            _u = u; 
            _config = config;
        }
        public async Task<string> CreateToken(User user)
        {
            // JwtSecurityTokenHandler => cho phép thao tác với token(tạo - gọi method - xác thực)
            var jwtHandeler = new JwtSecurityTokenHandler();
            // tạo key
            var key = Encoding.ASCII.GetBytes(_config["IssuerSigningKey"]);
            // tạo playload chứa name và role
       
            var userRole = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == user.Email);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,userRole.Role),
                new Claim(ClaimTypes.Name,$"{userRole.Name}")
            });
            //tạo chữ ký 
            var cerdential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            // 
            var des = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(3000),
                SigningCredentials = cerdential
            };
            var token = jwtHandeler.CreateToken(des);

            var AccessToken = jwtHandeler.WriteToken(token);
            Console.WriteLine(AccessToken);
            return AccessToken;
        }

        public async Task<ReFreshToken> createRrefreshtoken(int id)
        {        
            var existingToken = await _u.Repository<ReFreshToken>().GetById(id);
            if (existingToken != null)
            {
                existingToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                existingToken.BeginDate = DateTime.Now;
                existingToken.EndDate = DateTime.Now.AddSeconds(7000);
                _u.Repository<ReFreshToken>().UpDate(existingToken);
                return existingToken;
            }
            var refreshtoken = new ReFreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddSeconds(700000),
                UserId = id
            };
            await _u.Repository<ReFreshToken>().Create(refreshtoken);
            await _u.SaveChangesAsync();
            return refreshtoken;
        }

        /*
         test khi co font-end
         */
        public async Task<ReFreshToken> RefreshAccessToken(string token)
        {
            var unquiname = dataFormToken(token);         
            var user = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Name == unquiname);
            var findTokenWithUserId = await _u.Repository<ReFreshToken>().GetById(user.Id);   
            if (findTokenWithUserId.Token == null || findTokenWithUserId.EndDate <= DateTime.Now)
            {
                return null;
            }
            else
            { 
                var newRToken = await createRrefreshtoken(findTokenWithUserId.UserId);
                var newAccToken = await CreateToken(user);
                newRToken.AccessToken = newAccToken;
                return newRToken;
            }
        }

        public string dataFormToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = token.Replace("Bearer ", string.Empty);
            var readableToken = jwtHandler.CanReadToken(jwtInput);
            if (!readableToken) return null;
            var decodedJwt = jwtHandler.ReadJwtToken(jwtInput);
            var claimName = decodedJwt.Claims;
            foreach (var item in claimName)
            {
                if (item.Type == "unique_name")
                {
                    return item.Value;
                }
            }
            return "";
        }

    }
}
