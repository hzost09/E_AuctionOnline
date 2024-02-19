using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _u;
        private readonly IphotoService _p;
        private readonly IDocument _d;
        public UserService(IUnitOfWork u,IphotoService p,IDocument d)
        {
            _u = u;
            _p = p; 
            _d = d;
        }
        // get category list
        public async Task<IList<Category>> sendcategorylist()
        {
            try
            {
                var listcate = await _u.Repository<Category>().ListEntities();
                return listcate;    
            }
            catch (Exception ex) {
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
                            avatalink =  await _p.addPhoto(model.AvatarFile);
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
            catch(Exception ex) 
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
        public async Task<IList<Item>> searchCombine(string itemname,string categoryname)
        {
            try
            {
                var ListItem = new List<Item>();
                if (itemname != "" && categoryname == "")
                {
                    ListItem = await _u.Repository<Item>().EntitiesCondition()
                        .Where(x => x.Name.Contains(itemname))
                        .Select(x => new Item { Id = x.Id, Name = x.Name, Description = x.Description})
                        .ToListAsync();
                }
                if (itemname == "" && categoryname != "")   
                {
                    var cate = await _u.Repository<Category>().EntitiesCondition().FirstOrDefaultAsync(x => x.Name.Contains(categoryname));
                    var listCateItem = await _u.Repository<CateItem>().EntitiesCondition().Where(x => x.CateId == cate.Id).ToListAsync();
                    var listItemId = listCateItem.Select(x => x.ItemId).ToList();
                    ListItem = await _u.Repository<Item>().EntitiesCondition().Where(x => listItemId.Contains(x.Id)).ToListAsync();
                }
                if (itemname != "" && categoryname != "")
                {
                    var cate = await _u.Repository<Category>().EntitiesCondition().FirstOrDefaultAsync(x => x.Name.Contains(categoryname));
                    var listCateItem = await _u.Repository<CateItem>().EntitiesCondition().Where(x => x.CateId == cate.Id).ToListAsync();
                    var listItemId = listCateItem.Select(x => x.ItemId).ToList();
                    var ListItemBaseId = await _u.Repository<Item>().EntitiesCondition().Where(x => listItemId.Contains(x.Id)).ToListAsync();                      
                    ListItem = await _u.Repository<Item>().EntitiesCondition().Where(x => x.Name.Contains(itemname)).ToListAsync();
                }
                return ListItem;
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }

        //sell item
        public async Task<(Item, string)> sellItem(SellItemRequest model)
        {
            Item newItem = null;
            string result = "FailAction";

            try
            {
                // sell Item
                newItem = new Item();
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

                //
                if (model.CategoryModel.Count() == 0)
                {
                    return (null, result);
                }
                //push category to cateItem 
                foreach (var i in model.CategoryModel)
                {
                    var newCateItem = new CateItem();
                    newCateItem.CateId = i;
                    newCateItem.ItemId = newItem.Id;
                    await _u.Repository<CateItem>().Create(newCateItem);
                }

                // create Auction history
                var newAh = new AuctionHistory();
                newAh.EndDate = model.ItemModel.EndDate;
                newAh.ItemId = newItem.Id;
                await _u.Repository<AuctionHistory>().Create(newAh);

                await _u.SaveChangesAsync();
                result = "Success";
            }
            catch (Exception ex)
            {
                await _u.RollBackChangesAsync();
            }

            return (newItem, result);
        }


        ////test array category
        //public async Task<IList<CateItem>> testarray(CategoryModel[] model)
        //{
        //    CateItem newItem = null;
        //    List<CateItem> l = new List<CateItem>();
        //    foreach (var item in model)
        //    {
        //        newItem = new CateItem();
        //        newItem.CateId = item.Id;
        //        newItem.ItemId = 2;
        //        await _u.Repository<CateItem>().Create(newItem);
        //        l.Add(newItem);
        //    }
        //    await _u.SaveChangesAsync();
        //    return l;
        //}

        //Create Bid
        //public async Task<> PlaceBid(Bid model)
        //{
        //    try
        //    {
        //        var newbid = new Bid();
        //        newbid.BidAmount = model.BidAmount;
        //        newbid.ItemId = model.ItemId;
        //        newbid.UserId = model.UserId;
        //        newbid.BeginDate = model.BeginDate;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


    }
}
