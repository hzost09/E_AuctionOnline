using ApplicationLayer.InterfaceService;
using ApplicationLayer.Service;
using ApplicationLayer.Validation.LoginValid;
using ApplicationLayer.Validation.RegisterValid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer
{
    public static class ConfigService
    {
        public static IServiceCollection AddAppService(this IServiceCollection service, IConfiguration config)
        {
            //validate for login
            service.AddScoped<ILoginChain, LogInEmailValidate>();
            service.AddScoped<ILoginChain, LogInPaswordValidate>();
            // validate for signUp
            service.AddScoped<ISignUpChain, SignUpEmailValidate>();
            service.AddScoped<ISignUpChain, SignUpPaswordValidate>();
            service.AddScoped<ISignUpChain, SignUpUserNameValidate>();
            //validate for Item

            //Admin service
            service.AddScoped<IAdminService, AdminService>();
            // Auth service
            service.AddScoped<IAuthService, AuthService>();
            //jwt service
            service.AddScoped<IJwtService, JWTservice>();
            return service;
        }
    }
}
