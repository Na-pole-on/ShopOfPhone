using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(string id);
        T GetById(string id);
        Task<bool> CreateAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(string id);
    }
}
