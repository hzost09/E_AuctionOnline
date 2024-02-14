using DomainLayer.Core;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using InfrastructureLayer.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repos
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _data;
        public Repository(DataContext data)
        {
            _data = data;   
        }
        //getAll 
        public async Task<IList<T>> ListEntities()
        {
            return await _data.Set<T>().ToListAsync();
        }

        // allow using entityFramework for IRepository
        public IQueryable<T> EntitiesCondition()
        {
            return _data.Set<T>();
        }

        //get one by id
        public async Task<T> GetById(int id)
        {
            var Entities = await _data.Set<T>().FindAsync(id);
            return Entities;
        }

        //Create
        public async Task<T> Create(T entity)
        {
            await _data.Set<T>().AddAsync(entity);
            return entity;
        }

        //update
        public void UpDate(T entity)
        {
            _data.Set<T>().Update(entity);
        }

        //delete
        public void Delete(T entity)
        {
            _data.Remove(entity);
        }

        //get by User by email
        public async Task<T> GetUserByEmail<T>(string email) where T : User 
        {
            var entities = await _data.Set<T>().FirstOrDefaultAsync(x => x.Email == email);
            return entities;
        }

        // get by User by Name
        public async Task<T> GetUserByName<T>(string Name) where T : User
        {
            var entities = await _data.Set<T>().FirstOrDefaultAsync(x => x.Name == Name);
            return entities;
        }
        
        // get item by name
        public async Task<IList<T>> getItembyName<T>(string name) where T : Item
        {
            var entities = await _data.Set<T>().Where(x => x.Name.Contains(name)).ToListAsync();
            return entities;
        }
        
    }
}
