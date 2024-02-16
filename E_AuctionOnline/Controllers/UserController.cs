using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_AuctionOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm]UserModel model)
        {
            var checkupdateUser = await _user.UpdateUser(model);
            if (checkupdateUser != 1 )
            {
                return BadRequest(new
                {
                    message = "Fail Action"
                });
            }          
            return Ok(checkupdateUser);
        }
    }
}
