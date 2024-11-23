using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.reposatories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcProjectDbContext dbContext;

        public EmployeeRepository(MvcProjectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return dbContext.employees.Where(E => E.Address == address);  
            
        }

        public IQueryable<Employee> GetEmployeesByName(string SearchValue)
        {
            return dbContext.employees.Where(E => E.Name.ToLower().Contains(SearchValue.ToLower()));
        }
    }
}
