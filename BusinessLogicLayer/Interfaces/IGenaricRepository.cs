using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenaricRepository<T>
    {
        Task <IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T Item);
        Task Update(T Item);
        Task Delete(T Item);
    }
}
