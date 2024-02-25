using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using ApplicationLayer.Validation.LoginValid;
using ApplicationLayer.Validation.RegisterValid;
using DomainLayer.Imterface;
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
        private readonly IUnitOfWork _u;
        private readonly IResetEmailService _r;
        public AuthController(IAuthService auth, IJwtService j,IUnitOfWork u,IResetEmailService r)
        {
            _auth = auth;
            _j = j; 
            _u = u; 
            _r = r;
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
                return BadRequest(new { message = ex.Message.ToString() });
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
                signUpvalidator.AddValidator(new SignUpEmailValidate(_u));
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
                    var createVerifyToken = await _j.createVerifytoken(model.Email);
                    var mailmodel = await _r.SendEmailtoVerify(createVerifyToken);
                     _r.sendMail(mailmodel);
                    return Ok(new
                    {
                        message = signUpAction.Item2
                    });
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Route("checkemailandsendlink")]
        [HttpPost]
        public async Task<IActionResult> checkemailandsendlink(string email)
        {
            var checkEmail = await _r.CheckEmailAndTokenEmail(email);
            if (checkEmail == null)
            {
                return BadRequest(new
                {
                    message = "No Email exit"
                });
            }
            if (_r.sendMail(checkEmail))
            {
                return Ok(new
                {
                    message = "Success Action"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = "Fail to send Reset link"
                });
            }
        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassWordModel model)
        {
            var checkPasswordReset = await _r.checkTokenEmailAndSaveNewPassword(model);
            if (checkPasswordReset == 0)
            {
                return BadRequest(new
                {
                    message = "Fail Action"
                });
            }
            else if (checkPasswordReset == 1)
            {
                return Ok(new
                {
                    message = "Success Action"
                });
            }
            else if (checkPasswordReset == -1)
            {
                return BadRequest(new
                {
                    message = "Token Expire Or invalid Token"
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("verify")]
        [HttpPost]
        public async Task<IActionResult> verifyEmail(verifymodel model)
        {
            var result = await _auth.confrimVerify(model);
            if (result.Item1 == -1)
            {
                return BadRequest();
            }
            return Ok(result.Item2);
        }
    }
}
