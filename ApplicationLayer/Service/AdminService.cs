using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using Azure;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    internal class AdminService: IAdminService
    {
        private readonly IUnitOfWork _u;
        public AdminService(IUnitOfWork u)
        {
            _u = u;
        }
        //listUser with solditem count
        public async Task<(IDictionary<User,int>,int)> getListUser(int page, int pagesize)
        {
            try
            {
                var totalcount = await _u.Repository<User>().EntitiesCondition().Include(x => x.soldItem).Where(x => x.Role == "User").ToListAsync();
                var totalPage = (int)Math.Ceiling((decimal)totalcount.Count / pagesize);
                var UserPerPage = totalcount.Skip((page - 1) * pagesize)
                                             .Take(pagesize)
                                             .ToList();

                var listUserWithCountItem = new Dictionary<User, int>();
                
                foreach (var item in UserPerPage)
                {
                    var count = item.soldItem.Count;
                    listUserWithCountItem.Add(item,count);
                }
                return (listUserWithCountItem,totalcount.Count);
            }
            catch (Exception ex)
            {
                return (null,0);
            }
    
        }
        
        //listitem with DTO model
        public async Task<IList<ItemModel>> getListItem(int page, int pagesize)
        {
                var totalcount = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).ToListAsync();
                var totalPage = (int)Math.Ceiling((decimal)totalcount.Count / pagesize);
                var ItemPerPage = totalcount.Skip((page - 1) * pagesize)
                                             .Take(pagesize)
                                             .ToList();               
                var listItem = new List<ItemModel>();
                foreach (var item in ItemPerPage)
                {
                var itemmodel = new ItemModel();
                    itemmodel.Id = item.Id;
                    itemmodel.Name = item.Name;
                    itemmodel.Description = item.Description;
                    itemmodel.Email = item.Seller.Email;                  
                    listItem.Add(itemmodel);
                } 
                return listItem;
        }

        //wait for Font-end to check again
        //OneItem With ListCategory
        public async Task<(Item,IList<(Category,bool)>)> GetOneItemWithListCategory(int id)
        {
           
            var listCategory = new List<(Category, bool)>();
            var getListCategory = await _u.Repository<Category>().EntitiesCondition().Include(x => x.cateItems).ToListAsync();
            var getItem = await _u.Repository<Item>().GetById(id);
            foreach (var item in getListCategory)
            {
               var selectItem =  item.cateItems.Any(x => x.ItemId == getItem.Id);
                listCategory.Add((item, selectItem));
            }
            return (getItem, listCategory);
        }
        
        //lock or unlock user
        public async Task<(bool,string)> LockOrUnlockUser(int id)
        {
            try
            {
                var findUser = await _u.Repository<User>().GetById(id);
                if (findUser == null)
                {
                    return (false, "Cant find User");
                }
                if (findUser.Role == "User")
                {
                    findUser.Role = "Disable";
                    await _u.SaveChangesAsync();
                    return (true,"Changed to Disable");
                }
                else if (findUser.Role == "Disable")
                {
                    findUser.Role = "User";
                    await _u.SaveChangesAsync();
                    return (true, "Changed to User");
                }
                else
                {
                    return (false,"Fail Actions");
                }
            }
            catch (Exception ex)
            {
                await _u.RollBackChangesAsync();
                return (false, "Fail Actions");
            }
           
        }

        //search item name
        public async Task<IList<ItemModel>> SearchbyName(string name)
        {
            try
            {
                var items = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).Where(x => x.Name.Contains(name)).ToListAsync();           
                var itemmodels = new List<ItemModel>();
                foreach (var item in items)
                {
                    var itemmodel = new ItemModel();
                    itemmodel.Id = item.Id;
                    itemmodel.Name = item.Name;
                    itemmodel.Description = item.Description;
                    itemmodel.Image = item.Image;
                    itemmodel.Email = item.Seller.Email;
                    itemmodels.Add(itemmodel);
                }
                return itemmodels;
            }
            catch (Exception e)
            {
                return null;
            }
      
        }

        // add_or_remove_item
        public async Task<bool> addOrDeleteItemForCate(int CategoryId, int ItemId)
        {
            try
            {               
                var findcateItem = await _u.Repository<CateItem>().EntitiesCondition().FirstOrDefaultAsync(x => x.CateId == CategoryId && x.ItemId == ItemId);
                if (findcateItem == null) { 
                    return false;
                }
                if (findcateItem == null)
                {
                    var CreatCateItem = new CateItem();
                    CreatCateItem.ItemId = ItemId;
                    CreatCateItem.CateId = CategoryId;
                    await _u.Repository<CateItem>().Create(CreatCateItem);
                }
                else
                {
                    _u.Repository<CateItem>().Delete(findcateItem);
                }
                await _u.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {       
                await _u.RollBackChangesAsync();    
                return false;
            }
        
        }

       
    }
}
