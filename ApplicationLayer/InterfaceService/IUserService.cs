using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    public interface IUserService
    {
        Task<IList<Category>> sendcategorylist();
        Task<int> UpdateUser(UserModel model);
        Task<User> GetUserProfile(string email);
        Task<(IList<Item>, IList<Bid>)> getlistItemOfUser(int id);
        Task<IList<Item>> searchCombine(string itemname, string categoryname);
        Task<(Item, string)> sellItem(SellItemRequest item);
        //Task<IList<CateItem>> testarray(CategoryModel[] model);


    }
}
