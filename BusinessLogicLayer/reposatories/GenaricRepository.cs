using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class GenericRepository<T> : IGenaricRepository<T> where T : class
    {
        private readonly MvcProjectDbContext dbContext;

        public GenericRepository(MvcProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(T item)
        {
            await dbContext.AddAsync(item);
        }

        public async Task Delete(T item)
        {
            dbContext.Remove(item);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await dbContext.employees.Include(e => e.department).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T item)
        {
            dbContext.Update(item);
            await Task.CompletedTask;
        }
    }
}
