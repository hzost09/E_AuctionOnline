using DomainLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Imterface
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task RollBackChangesAsync();
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}
