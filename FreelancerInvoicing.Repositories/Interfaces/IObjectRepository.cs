using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Repositories.Interfaces
{
    public interface IObjectRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllObjectAsync();
        Task<T> GetObjectByIdAsync(int id);
        Task AddObjectAsync(T entity);
        Task ModifyObjectAsync(T entity);
        Task DeletObjectAsync(int id);
    }
}
