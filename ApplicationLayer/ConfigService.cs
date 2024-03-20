using ApplicationLayer.InterfaceService;
using ApplicationLayer.Service;
using ApplicationLayer.Validation.ItemValid;
using ApplicationLayer.Validation.LoginValid;
using ApplicationLayer.Validation.RegisterValid;
using ApplicationLayer.Validation.UserValid;
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
            service.AddScoped<IItemValidChain,ItemNameValidate >();
            service.AddScoped<IItemValidChain, ItemPriceValidate>();
            service.AddScoped<IItemValidChain, ItemImageValidate>();
            service.AddScoped<IItemValidChain, ItemDocumentValidate>();
            service.AddScoped<IItemValidChain, ItemSellerIdValidate>();
            service.AddScoped<IItemValidChain, ItemDateValidate>();
            //validate for User
            service.AddScoped<IUserValidChain, UserValidEmail>();
            service.AddScoped<IUserValidChain, NameValidate>();

            service.AddScoped<ISignUpChain, SignUpPaswordValidate>();
            //Admin service
            service.AddScoped<IAdminService, AdminService>();
            // Auth service
            service.AddScoped<IAuthService, AuthService>();
            //jwt service
            service.AddScoped<IJwtService, JWTservice>();
            //user service
            service.AddScoped<IUserService, UserService>();
            //photo service
            service.AddScoped<IphotoService, PhotoService>();
            //document service
            service.AddScoped<IDocument, DocumentService>();
            return service;
        }
    }
}
