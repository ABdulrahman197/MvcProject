using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.reposatories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MvcProjectDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }


        public UnitOfWork(MvcProjectDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);

            this.dbContext = dbContext;
        }
        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
