using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using ApplicationLayer.Validation.LoginValid;
using ApplicationLayer.Validation.RegisterValid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace E_AuctionOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IJwtService _j;
        public AuthController(IAuthService auth, IJwtService j)
        {
            _auth = auth;
            _j = j; 

        }

        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            try
            {
                var loginvalidator = new LoginValidate();
                loginvalidator.AddValidator(new LogInEmailValidate());
                loginvalidator.AddValidator(new LogInPaswordValidate());
                await loginvalidator.ActionValid(model);
                var loginAction = await _auth.login(model);
                if (loginAction.Item1 == null)
                {
                    return NotFound(new
                    {
                        message = loginAction.Item2
                    });
                }
                else
                {
                  var AccessToken =  await _j.CreateToken(loginAction.Item1);
                    var RefreshToken =  await _j.createRrefreshtoken(loginAction.Item1.Id);
                    return Ok(new
                    {   
                        AccessToken = AccessToken,
                        RefreshToken = RefreshToken.Token,
                        message = loginAction.Item2
                    });
                }

            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var signUpvalidator = new SignUpValidate();
                signUpvalidator.AddValidator(new SignUpUserNameValidate());
                signUpvalidator.AddValidator(new SignUpEmailValidate());
                signUpvalidator.AddValidator(new SignUpPaswordValidate());
                await signUpvalidator.ActionValid(model);
                var signUpAction = await _auth.Register(model);
                if (signUpAction.Item1 == -1)
                {
                    return BadRequest(new
                    {
                        message = signUpAction.Item2
                    });
                }
                else
                {
                    return Ok(new
                    {
                        message = signUpAction.Item2
                    });
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
