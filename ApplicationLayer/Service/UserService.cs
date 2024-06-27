using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _u;
        private readonly IphotoService _p;
        private readonly IDocument _d;
        private readonly IResetEmailService _r;
        public UserService(IUnitOfWork u, IphotoService p, IDocument d, IResetEmailService r)
        {
            _u = u;
            _p = p;
            _d = d;
            _r = r;
        }
        //get list item
        //public async Task<(IList<ItemModel>, int)> getListItem(int page, int pagesize)
        //{


        //    var totalcount = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).Include(x => x.cateItems).ThenInclude(x => x.category).ToListAsync();
        //    var totalPage = (int)Math.Ceiling((decimal)totalcount.Count / pagesize);
        //    var itemPerPage = totalcount.Skip((page - 1) * pagesize)
        //                                 .Take(pagesize)
        //                                 .ToList();
        //    var listItem = new List<ItemModel>();
        //    foreach (var item in itemPerPage)
        //    {
        //        var itemmodel = new ItemModel();
        //        itemmodel.Id = item.Id;
        //        itemmodel.Name = item.Name;
        //        itemmodel.Description = item.Description;
        //        itemmodel.Email = item.Seller.Email;
        //        itemmodel.Image = item.Image;
        //        itemmodel.BeginPrice = item.BeginPrice;
        //        itemmodel.UpPrice = item.UpPrice;
        //        itemmodel.WinningPrice = item.WinningPrice;
        //        itemmodel.BeginDate = item.BeginDate;
        //        itemmodel.EndDate = item.EndDate;
        //        itemmodel.categoryName = item.;
        //        listItem.Add(itemmodel);
        //    }
        //    return (listItem, totalcount.Count);
        //}
        public async Task<(IList<ItemModel>, int)> getListItem(int page, int pagesize)
        {
            var totalcount = await _u.Repository<Item>().EntitiesCondition()
                                              .Include(x => x.Seller)
                                              .Include(x => x.cateItems)
                                                .ThenInclude(ci => ci.category) // Include category từ cateItems
                                              .ToListAsync();

            var totalPage = (int)Math.Ceiling((decimal)totalcount.Count / pagesize);
            var itemPerPage = totalcount.Skip((page - 1) * pagesize)
                                        .Take(pagesize)
                                        .ToList();
            var listItem = new List<ItemModel>();
            foreach (var item in itemPerPage)
            {
                var itemmodel = new ItemModel();
                itemmodel.Id = item.Id;
                itemmodel.Name = item.Name;
                itemmodel.Description = item.Description;
                itemmodel.Email = item.Seller.Email;
                itemmodel.Image = item.Image;
                itemmodel.BeginPrice = item.BeginPrice;
                itemmodel.UpPrice = item.UpPrice;
                itemmodel.WinningPrice = item.WinningPrice;
                itemmodel.BeginDate = item.BeginDate;
                itemmodel.EndDate = item.EndDate;
                itemmodel.categoryName = item.cateItems.Select(ci => ci.category.Name).ToList();
                listItem.Add(itemmodel);
            }
            return (listItem, totalcount.Count);
        }

        // get category list
        public async Task<IList<Category>> sendcategorylist()
        {
            try
            {
                var listcate = await _u.Repository<Category>().ListEntities();
                return listcate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //get user profile 
        public async Task<User> GetUserProfile(string email)
        {
            try
            {
                var findbyemail = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Email == email);
                if (findbyemail == null)
                {
                    return null;
                }
                else
                {
                    return findbyemail;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //update user 
        public async Task<int> UpdateUser(UserModel model)
        {
            try
            {
                var oldmodel = await _u.Repository<User>().GetById(model.Id);
                string avatalink = "";
                if (oldmodel != null)
                {
                    if (model.AvatarFile == null)
                    {
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                    }
                    else
                    {
                        if (oldmodel.Avatar != null)
                        {
                            var avataResult = await _p.DeletPhoto(oldmodel.Avatar);
                            avatalink = await _p.addPhoto(model.AvatarFile);
                        }
                        else
                        {
                            avatalink = await _p.addPhoto(model.AvatarFile);
                        }
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                        oldmodel.Avatar = avatalink;

                    }
                }
                await _u.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                await _u.RollBackChangesAsync();
                return -1;
            }


        }

        //get list item of user
        public async Task<(IList<Item>, IList<Bid>)> getlistItemOfUser(int sellerid)
        {
            var listItem = await _u.Repository<Item>().EntitiesCondition().Where(x => x.sellerId == sellerid).ToListAsync();
            var listId = listItem.Select(item => item.Id).ToList();

            var listBid = await _u.Repository<Bid>().EntitiesCondition().Where(x => listId.Contains(x.ItemId)).ToListAsync();

            return (listItem, listBid);
        }

        //search item by Name Or by category or both
        public async Task<IList<ItemModel>> searchCombine(string itemName, string categoryName)
        {
            try
            {
                var listItem = new List<Item>();
                var listItemModel = new List<ItemModel>();
                if (itemName == null && categoryName == null)
                {
                    listItem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller)
                        .Include(x => x.cateItems)
                         .ThenInclude(ci => ci.category)
                        .ToListAsync();
                    foreach (var item in listItem)
                    {
                        var itemmodel = new ItemModel();
                        itemmodel.Id = item.Id;
                        itemmodel.Name = item.Name;
                        itemmodel.Description = item.Description;
                        itemmodel.Email = item.Seller.Email;
                        itemmodel.Image = item.Image;
                        itemmodel.BeginPrice = item.BeginPrice;
                        itemmodel.UpPrice = item.UpPrice;
                        itemmodel.WinningPrice = item.WinningPrice;
                        itemmodel.BeginDate = item.BeginDate;
                        itemmodel.EndDate = item.EndDate;
                        itemmodel.categoryName = item.cateItems.Select(ci => ci.category.Name).ToList();
                        listItemModel.Add(itemmodel);
                    }
                    return listItemModel;
                }
                if (itemName == null && categoryName != "")
                {
                    var cate = await _u.Repository<Category>().EntitiesCondition().FirstOrDefaultAsync(x => x.Name.Contains(categoryName));
                    var listCateItem = await _u.Repository<CateItem>().EntitiesCondition().Where(x => x.CateId == cate.Id).ToListAsync();
                    var listItemId = listCateItem.Select(x => x.ItemId).ToList();
                    listItem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).Where(x => listItemId.Contains(x.Id)).ToListAsync();
                    foreach (var item in listItem)
                    {
                        var itemmodel = new ItemModel();
                        itemmodel.Id = item.Id;
                        itemmodel.Name = item.Name;
                        itemmodel.Description = item.Description;
                        itemmodel.Email = item.Seller.Email;
                        itemmodel.Image = item.Image;
                        itemmodel.BeginPrice = item.BeginPrice;
                        itemmodel.UpPrice = item.UpPrice;
                        itemmodel.WinningPrice = item.WinningPrice;
                        itemmodel.BeginDate = item.BeginDate;
                        itemmodel.EndDate = item.EndDate;
                        itemmodel.categoryName = item.cateItems.Select(ci => ci.category.Name).ToList();
                        listItemModel.Add(itemmodel);
                    }
                    return listItemModel;
                }
                if (itemName != null && categoryName == null)
                {                                      
                    listItem = await _u.Repository<Item>().EntitiesCondition().Where(x => x.Name.Contains(itemName))
                        .Include(x => x.cateItems)
                        .Include(x => x.Seller).ToListAsync();
                    foreach (var item in listItem)
                    {
                        var itemmodel = new ItemModel();
                        itemmodel.Id = item.Id;
                        itemmodel.Name = item.Name;
                        itemmodel.Description = item.Description;
                        itemmodel.Email = item.Seller.Email;
                        itemmodel.Image = item.Image;
                        itemmodel.BeginPrice = item.BeginPrice;
                        itemmodel.UpPrice = item.UpPrice;
                        itemmodel.WinningPrice = item.WinningPrice;
                        itemmodel.BeginDate = item.BeginDate;
                        itemmodel.EndDate = item.EndDate;
                        itemmodel.categoryName = item.cateItems.Select(ci => ci.category.Name).ToList();
                        listItemModel.Add(itemmodel);
                    }
                    return listItemModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //sell item
        public async Task<(Item, string)> sellItem(SellItemRequest model)
        {
            try
            {
                // sell Item
                var newItem = new Item();
                newItem.Image = await _p.addPhoto(model.ItemModel.ImageFile);
                newItem.Document = await _d.WriteFile(model.ItemModel.DocumentFile);
                newItem.Name = model.ItemModel.Name;
                newItem.Description = model.ItemModel.Description;
                newItem.BeginPrice = model.ItemModel.BeginPrice;
                newItem.UpPrice = model.ItemModel.UpPrice;
                newItem.WinningPrice = model.ItemModel.WinningPrice;
                newItem.BeginDate = model.ItemModel.BeginDate;
                newItem.EndDate = model.ItemModel.EndDate;
                newItem.sellerId = model.ItemModel.sellerId;
                await _u.Repository<Item>().Create(newItem);
                await _u.SaveChangesAsync();

                foreach (var i in model.CategoryModel)
                {
                    var newCateItem = new CateItem();
                    newCateItem.CateId = i;
                    newCateItem.ItemId = newItem.Id;
                    await _u.Repository<CateItem>().Create(newCateItem);
                }
                await _u.SaveChangesAsync();
                // create Auction history
                var newAh = new AuctionHistory();
                newAh.EndDate = model.ItemModel.EndDate;
                newAh.HighestBid = 0;
                newAh.ItemId = newItem.Id;
                await _u.Repository<AuctionHistory>().Create(newAh);
                await _u.SaveChangesAsync();
                return (newItem, "Success");
            }
            catch (Exception ex)
            {

                await _u.RollBackChangesAsync();
                return (null, "Fail Action");
            }


        }

        //Create Bid
        public async Task<(AuctionHistory, string)> PlaceBid(BidModel model)
        {
            try
            {
                var takeitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Auctionhistory).FirstOrDefaultAsync(x => x.Id == model.ItemId);
                var takeAuctionhistory = await _u.Repository<AuctionHistory>().EntitiesCondition().FirstOrDefaultAsync(x => x.ItemId == takeitem.Id);
                // create bid
                var newbid = new Bid();
                newbid.BidAmount = model.BidAmount;
                newbid.ItemId = model.ItemId;
                newbid.UserId = model.UserId;
                newbid.BeginDate = DateTime.Now;
                await _u.Repository<Bid>().Create(newbid);

                await _u.SaveChangesAsync();
                if (takeAuctionhistory.WinnerId != null)
                {
                    return (takeAuctionhistory, "winner exits");
                }

                if (model.BidAmount >= takeitem.WinningPrice)
                {
                    takeAuctionhistory.HighestBid = model.BidAmount;
                    takeAuctionhistory.WinnerId = model.UserId;
                    _u.Repository<AuctionHistory>().UpDate(takeAuctionhistory);
                    await _u.SaveChangesAsync();
                    return (takeAuctionhistory, "you are the winner");
                }
                
                // update bidAmout for Auction history 
                takeAuctionhistory.HighestBid = model.BidAmount;
                _u.Repository<AuctionHistory>().UpDate(takeAuctionhistory);
                return (takeAuctionhistory, "Success Action");
            }
            catch (Exception ex)
            {
                await _u.RollBackChangesAsync();
                return (null, "Fail Action");
            }
        }

        //take all bid base on id item

        public async Task<(List<Bid>,string)> getAllBid(int itemId)
        {
            try
            {
                //find all bid have item id
                List<Bid> listBid = await _u.Repository<Bid>().EntitiesCondition().Where(x => x.ItemId == itemId)
                    .Include(x => x.User)
                    .ToListAsync();
                listBid.Select(b => b.User?.Avatar).ToList();
                return (listBid,"success");
            }
            catch (Exception e)
            {            
                return (null,"error");
            }
        }

        // take one item
        public async Task<ItemModel> getOneItem(int id)
        {
            try
            {
                var getitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller)
                                       
                                        .Include(x => x.cateItems)
                                         .ThenInclude(x => x.category)
                                        .FirstOrDefaultAsync(x => x.Id == id);
                ItemModel model = new ItemModel();
                if (getitem != null)
                {
                    model.Id = getitem.Id;
                    model.Name = getitem.Name;
                    model.Description = getitem.Description;
                    model.BeginPrice = getitem.BeginPrice;
                    model.UpPrice = getitem.UpPrice;
                    model.WinningPrice = getitem.WinningPrice;
                    model.Document = getitem.Document;
                    model.Image = getitem.Image;
                    model.Email = getitem.Seller.Email;
                    model.sellerId = getitem.sellerId;
                    model.BeginDate = getitem.BeginDate;
                    model.categoryName = getitem.cateItems.Select(x => x.category.Name).ToList();
                    model.EndDate = getitem.EndDate;
                    model.Document = getitem.Document;
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //rating
        public async Task<(bool, string)> RattingUser(string name, RateBuyerModel model)
        {
            try
            {
                var checkname = await _u.Repository<User>().EntitiesCondition().FirstOrDefaultAsync(x => x.Name == name);
                if (checkname.Id == model.sellerId)
                {
                    return (false, "you cant rate your own");
                }
                var checkitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).FirstOrDefaultAsync(x => x.Id == model.ItemId);
                if (checkname.Id == checkitem.sellerId)
                {
                    return (false, "you cant rate your own");
                }
                var Ratting = new Rating();
                Ratting.RatingDate = DateTime.Now;
                Ratting.RateUserId = checkname.Id;
                Ratting.SellerId = model.sellerId;
                Ratting.ItemId = model.ItemId;
                Ratting.Rate = model.RatingAmount;
                await _u.Repository<Rating>().Create(Ratting);
                await _u.SaveChangesAsync();
                return (true, "success");
            }
            catch (Exception ex)
            {
                await _u.RollBackChangesAsync();
                return (false, "Fail actions");
            }
        }

      
        public async Task<bool> AuctionEnd(int Itemid)
        {
            try
            {
                var takeItem = await _u.Repository<Item>().EntitiesCondition()
                                        .Include(x => x.Auctionhistory)
                                        .FirstOrDefaultAsync(x => x.Id == Itemid);
                if (takeItem.Auctionhistory.WinnerId == 0)
                {
                    return false;
                }
                var winner = takeItem.Auctionhistory.WinnerId;
                var seller = takeItem.sellerId;
                var itemname = takeItem.Name;
                _r.sendMail(await _r.sendMailForSuccessSeller(winner, seller, itemname));
                _r.sendMail(await _r.sendMailForSuccessBuyer(winner, seller, itemname));              
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}

