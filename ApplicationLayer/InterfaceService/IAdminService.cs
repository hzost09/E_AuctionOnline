using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    public interface IAdminService
    {
        Task<(IDictionary<User, int>, int)> getListUser(int page, int pagesize);
        Task<IList<ItemModel>> getListItem(int page, int pagesize);
        Task<(Item, IList<(Category, bool)>)> GetOneItemWithListCategory(int id);
        Task<IList<ItemModel>> SearchbyName(string name);
        Task<(bool,string)> LockOrUnlockUser(int id);
        Task<bool> addOrDeleteItemForCate(int CategoryId, int ItemId);
    }
}
