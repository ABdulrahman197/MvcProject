﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {
       IQueryable<Employee> GetEmployeesByAddress(string Address); 
        
       IQueryable<Employee> GetEmployeesByName(string SearchValue);

        

    }
}
