﻿using ApplicationLayer.InterfaceService;
using DomainLayer.Core.Enities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_AuctionOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static object _locker = new object();
        private readonly IAdminService _ad;
        public AdminController(IAdminService ad)
        {
            _ad = ad;
        }

        [Route("getlistUserWithItemCount")]
        [HttpGet]
        public async Task<IActionResult> getlistUserWithItemCount([FromQuery] int page, [FromQuery] int pagesize = 5)
        {
            var listUser = await _ad.getListUser(page, pagesize);
            if (listUser.Item1 == null)
            {
                return BadRequest(new
                {
                    message = "Fail Action"
                });
            }
            return Ok(new
            {
                listuser = listUser.Item1.Select(x => new
                {
                    user = x.Key,
                    itemCount = x.Value

                }),
                TotalUserCount = listUser.Item2
            });
        }

        [Route("listcategory")]
        [HttpGet]
        public async Task<IActionResult> getlistcategory()
        {
            var listcategory = await _ad.categorylist();
            if (listcategory != null)
            {
                return Ok(listcategory);
            }
            else
            {
                return NotFound(new
                {
                    message = "Fail Action"
                });
            }
        }

        [Route("getlistItem")]
        [HttpGet]
        public async Task<IActionResult> getlistItem([FromQuery] int page, [FromQuery] int pagesize = 5)
        {
            var listitem = await _ad.getListItem(page, pagesize);
            if (listitem.Item1 == null)
            {
                return BadRequest(new
                {
                    message = "Fail Actions"
                });
            }
            return Ok(new
            {
                listitem = listitem.Item1,
                ItemCount = listitem.Item2
            }) ;
        }

        [Route("ItemWithCategoryList/{id}")]
        [HttpGet]
        public async Task<IActionResult> ItemWithCategoryList(int id)
        {
            var Item = await _ad.GetOneItemWithListCategory(id);
            if (Item.Item1 == null)
            {
                return NotFound(new
                {
                    message = "Item not exits"
                });
            }
            else
            {
                var categoryList = Item.Item2.Select(x => new
                {
                    categories = x.Item1,
                    Belong = x.Item2
                }).ToList();
                return Ok(new
                {
                    Item = Item.Item1,
                    Categories = categoryList
                });
            }
        }

        [Route("SearchItem/{ItemName}")]
        [HttpGet]
        public async Task<IActionResult> SearchItem(string ItemName)
        {
            var ListItemBySearch = await _ad.SearchbyName(ItemName);
            if (ListItemBySearch.Count <= 0 )
            {
                return Ok(new
                {
                    message = "There Nothing like item you want to find"
                });
            }
            return Ok(ListItemBySearch);
        }

        [Route("LockUnlockUser/{userId}")]
        [HttpPost]
        public async Task<IActionResult> LockUnlockUser(int userId)
        {
            var checkstatus = await _ad.LockOrUnlockUser(userId);
            if (checkstatus.Item1 == true)
            {
                return Ok(new
                {
                    messsage = checkstatus.Item2
                });
            }
            else
            {
                return NotFound(new
                {
                    message = checkstatus.Item2
                });
            }
        }

        // add+remove item in cate
        [Route("AddDeleteCategoryItem/{CategoryId}/{ItemId}")]
        //[Route("AddDeleteCategoryItem")]
        [HttpPost]
        public async Task<IActionResult> AddDeleteCategoryItem( int CategoryId,int ItemId)
        {
            if (CategoryId == 0 || ItemId == 0)
            {
                return BadRequest(new
                {
                    message = "Fail Action"
                });
            }
            try
            {
                lock (_locker)
                {
                    var checkAddorDel = _ad.addOrDeleteItemForCate(CategoryId, ItemId).Result;
                    if (checkAddorDel)
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
                            message = "Fail Action"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Internal Server Error"
                });
            }
        }
        
        //create category
        [Route("CreateCategory")]
        [HttpPost]
        public async Task<IActionResult> createCategory(Category cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = ModelState});
            }
            var result = await _ad.CreateCategory(cate);
            if (result.Item1 == -1)
            {
                return BadRequest(new
                {
                    message = result.Item2
                });
            }
            else
            {
                return Ok(new
                {
                    message = result.Item2
                });
            }
        }

        //update category
        [Route("UpdateCategory")]
        [HttpPost]        
        public async Task<IActionResult> UpdateCategory(Category Cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState });
            }
            var checkcategory = await _ad.UpdateCategory(Cate);
            if (checkcategory.Item1 == null)
            {
                return BadRequest(new
                {
                    message = checkcategory.Item2
                });
            }
            else
            {
                return Ok(new
                {
                    message = checkcategory.Item2
                });
            }
        }

        //category with listitem
        [Route("categorywithlistitem/{categoryid}")]
        [HttpGet]
        public async Task<IActionResult> categorylistitem(int categoryid)
        {
            var result = await _ad.categoryWithitemList(categoryid);
            if(result.Item1 == null) {
                return BadRequest(new
                {
                    message = "Cant Find category"
                });
            }
            else
            {
                return Ok(new
                {
                    Category = result.Item1,
                    listItem = result.Item2 
                });
            }

        }
    }
}
