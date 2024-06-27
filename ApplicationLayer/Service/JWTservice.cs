using ApplicationLayer.InterfaceService;
using DomainLayer.Core;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
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
        
            var jwtHandeler = new JwtSecurityTokenHandler();
           
            var key = Encoding.ASCII.GetBytes(_config["IssuerSigningKey"]);
        
       
            var userRole = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == user.Email);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,userRole.Role),
                new Claim(ClaimTypes.Name,$"{userRole.Name}"),
                 new Claim(ClaimTypes.Email,$"{userRole.Email}")
            });
            //tạo chữ ký 
            var cerdential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            // 
            var des = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = cerdential
            };
            var token = jwtHandeler.CreateToken(des);

            var AccessToken = jwtHandeler.WriteToken(token);
            Console.WriteLine(AccessToken);
            return AccessToken;
        }

        public async Task<ReFreshToken> createRrefreshtoken(int id)
        {        
            var existingToken = await _u.Repository<ReFreshToken>().EntitiesCondition().FirstOrDefaultAsync(x => x.UserId == id);
            if (existingToken != null)
            {
                existingToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                existingToken.BeginDate = DateTime.Now;
                existingToken.EndDate = DateTime.Now.AddSeconds(400);
                _u.Repository<ReFreshToken>().UpDate(existingToken);             
                await _u.SaveChangesAsync();
                return existingToken;
            }
            var refreshtoken = new ReFreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddSeconds(200),
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
            var findTokenWithUserId = await _u.Repository<ReFreshToken>().EntitiesCondition().FirstOrDefaultAsync(x => x.UserId == user.Id);   
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

        public async Task<VerifyEmail> createVerifytoken(string email)
        {
            var findUser = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == email);
            if (findUser == null)
            {
                return null;
            }
            var existingToken = await _u.Repository<VerifyEmail>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == findUser.Email);
            if (existingToken != null)
            {
                existingToken.VerifyToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                existingToken.BeginDate = DateTime.Now;
                existingToken.EndDate = DateTime.Now.AddSeconds(70000);
                 _u.Repository<VerifyEmail>().UpDate(existingToken);
                await _u.SaveChangesAsync();
                return existingToken;
            }
            var verifytoken = new VerifyEmail()
            {
                VerifyToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddSeconds(70000),
                Email = findUser.Email,
                UserId = findUser.Id
            };
            await _u.Repository<VerifyEmail>().Create(verifytoken);
            await _u.SaveChangesAsync();
            return verifytoken;
        }
    }
}
