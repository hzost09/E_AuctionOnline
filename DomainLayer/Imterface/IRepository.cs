using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Imterface
{
    public interface IRepository<T>
    {
        Task<IList<T>> ListEntities();
        Task<T> GetById(int id);
        Task<T> GetUserByEmail<T>(string email) where T : User;
        Task<T> GetUserByName<T>(string Name) where T : User;
       
        Task<T> Create(T entity);
        void UpDate(T entity);
        void Delete(T entity);
        //public Task<IList<T>> getItembyName<T>(string name) where T : Item;
        IQueryable<T> EntitiesCondition();

    }
}
