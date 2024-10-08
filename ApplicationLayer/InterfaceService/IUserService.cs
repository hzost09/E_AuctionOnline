﻿using ApplicationLayer.DTO;
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
        Task<(IList<ItemModel>, int)> getListItem(int page, int pagesize);
        Task<IList<Category>> sendcategorylist();
        Task<int> UpdateUser(UserModel model);
        Task<User> GetUserProfile(string email);
        Task<(IList<Item>, IList<Bid>)> getlistItemOfUser(int id);
        Task<IList<ItemModel>> searchCombine(string itemname, string categoryname);
        Task<(Item, string)> sellItem(SellItemRequest item);
        Task<(AuctionHistory, string)> PlaceBid(BidModel model);
        
        Task<(List<Bid>,string)> getAllBid(int itemId);
        
        Task<ItemModel> getOneItem(int id);
        Task<(bool, string)> RattingUser(string name, RateBuyerModel model);
        Task<bool> AuctionEnd(int Itemid);

    }
}
