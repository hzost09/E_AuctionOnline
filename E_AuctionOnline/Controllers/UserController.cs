using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using ApplicationLayer.Validation.BidValid;
using ApplicationLayer.Validation.ItemValid;
using ApplicationLayer.Validation.UserValid;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace E_AuctionOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IUnitOfWork _u;
        private readonly IResetEmailService _r;
        private readonly IJwtService _j;
        public UserController(IUserService user, IUnitOfWork u, IResetEmailService r, IJwtService j)
        {
            _user = user;
            _u = u;
            _r = r;
            _j = j;
        }

        [Route("getlistcategory")]
        [HttpGet]
        public async Task<IActionResult> getlistCategory()
        {
            var listcategory = await _user.sendcategorylist();
            if (listcategory == null)
            {
                return  BadRequest(new
                {
                    message = "error when take category"
                });
            }
            return Ok(listcategory);
        }

        [Route("getProfile")]
        [HttpPost]
        public async Task<IActionResult> getProfile(string email)
        {
            var findUser = await _user.GetUserProfile(email);
            if (findUser == null)
            {
                return NotFound(new {message = "cant not find user"});
            }
            return Ok(findUser);
        }

        // need send new AccessToken to Client
        //User
        [Authorize(Roles = "User")]
        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm]UserModel model)
        {
            try
            {
                var modelcheck = new UserValid();
                modelcheck.AddValidator(new UserValidEmail(_u));
                modelcheck.AddValidator(new NameValidate());
                await modelcheck.ActionValid(model);
                var checkupdateUser = await _user.UpdateUser(model);
                if (checkupdateUser != 1)
                {
                    return BadRequest(new
                    {
                        message = "Fail Action"
                    });
                }
                return Ok(checkupdateUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
           
        }

        //User
        [Authorize(Roles = "User")]
        [Route("GetListItemOfUser/{id}")]
        [HttpGet]            
        public async Task<IActionResult> ListItemWithUserId(int id)
        {
            var listitem = await _user.getlistItemOfUser(id);
            return Ok(new { 
                ListItem = listitem.Item1,
                ListBid = listitem.Item2,
            });
        }

        [Route("SearchCombine")]
        [HttpPost]
        public async Task<IActionResult> SearchItemList(searchModel model)
        {
            var listsearch = await _user.searchCombine(model.ItemName,model.CategoryName);
            if (listsearch == null || listsearch.Count <= 0)
            {
                return NotFound(new
                {
                    message = "Cant find Item"
                });
            }
            return Ok(listsearch);
        }
        
        //User
        [Authorize(Roles = "User")]
        [Route("SellItem")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> sellItem([FromForm] SellItemRequest model)
        { 
            try
            {
                var checkItem = new ItemValid();
                checkItem.AddValidator(new ItemNameValidate());
                checkItem.AddValidator(new ItemPriceValidate());
                checkItem.AddValidator(new ItemImageValidate());
                checkItem.AddValidator(new ItemDocumentValidate());
                checkItem.AddValidator(new ItemDateValidate());
                checkItem.AddValidator(new ItemSellerIdValidate());
                await checkItem.ActionValid(model.ItemModel);
         

                var addModel = await _user.sellItem(model);
                return Ok(new
                {
                    message = addModel.Item2
                });
               
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }

        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        //User
        [Authorize(Roles = "User")]
        [Route("Placebid")]
        [HttpPost]
        public async Task<IActionResult> PlaceBid(BidModel model)
        {
            try
            {
                var checkbidmodel = new BidValid();
                checkbidmodel.AddValidator(new BidAmountValid(_u));
                checkbidmodel.AddValidator(new BidTimeValid(_u));
                checkbidmodel.AddValidator(new BidWinnerValid(_u));
                await checkbidmodel.ActionValid(model);
                var result = await _user.PlaceBid(model);
                if (result.Item1.WinnerId != 0)
                {
                    await _user.AuctionEnd(model.ItemId);  
                    return Ok(new
                    {
                        message = result.Item2
                    });
                }                
                return Ok(new
                {
                    message = result.Item2
                });
            }
            catch (ValidationException e)
            {
                return BadRequest(new { messsage = e.Message.ToString() });
            }
        }

        [Route("GetOneItem/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetOneItem(int id)
        {
            var result = await _user.getOneItem(id);
            if (result == null )
            {
                return NotFound(new
                {
                    message = "Cant Found Item"
                });
            }
            return Ok(result);
        }


        [Route("Rating")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Ratting([FromBody] RateBuyerModel req)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            var username = _j.dataFormToken(token);
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.ToString() });
            }
            var RateAction = await _user.RattingUser(username, req);
            if (RateAction.Item1 == false)
            {
                return BadRequest(new
                {
                    message = RateAction.Item2
                });
            }
            else
            {
                return Ok(new
                {
                    message = RateAction.Item2
                });
            }
        }


        //[Route("test")]
        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> Test([FromForm] SellItemRequest categoryModels)
        //{
        //    return Ok(categoryModels.CategoryModel);
        //}

    }
}
