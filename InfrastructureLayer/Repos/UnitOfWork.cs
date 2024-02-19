using DomainLayer.Core;
using DomainLayer.Imterface;
using InfrastructureLayer.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;
        private readonly IDictionary<Type, dynamic> _listRepos;
        public UnitOfWork(DataContext db)
        {
            _db = db;
            _listRepos = new Dictionary<Type, dynamic>();
        }
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var entityType = typeof(T);

            if (_listRepos.ContainsKey(entityType))
            {
                return _listRepos[entityType];
            }

            var repositoryType = typeof(Repository<>);

            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);

            if (repository == null)
            {
                throw new NullReferenceException("Repository should not be null");
            }

            _listRepos.Add(entityType, repository);

            return (IRepository<T>)repository;
        }

        public async Task RollBackChangesAsync()
        {
            await _db.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
