using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.reposatories
{
    public class DepartmentRepository : GenericRepository<Department>   , IDepartmentRepository
    {
        private readonly MvcProjectDbContext dbContext;

        public DepartmentRepository(MvcProjectDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }


        public IQueryable<Department> GetDepartmentByName(string SearchValue)
        {
            return dbContext.Departments.Where(D => D.Name.ToLower().Contains(SearchValue.ToLower()));
        }

        
    }
}

