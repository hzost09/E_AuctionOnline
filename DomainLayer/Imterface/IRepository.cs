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
        Task<T> GetById(int? id);         
        Task<T> Create(T entity);
        void UpDate(T entity);
        void Delete(T entity);
        IQueryable<T> EntitiesCondition();
 
    }
}
