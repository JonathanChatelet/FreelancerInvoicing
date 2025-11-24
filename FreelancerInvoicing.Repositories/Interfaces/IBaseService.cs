using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Repositories.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllObjectServiceAsync();
        Task<T> GetObjectByIdServiceAsync(int id);
        Task AddObjectServiceAsync(T entity);
        Task ModifyObjectServiceAsync(T entity);
        Task DeletObjectServiceAsync(int id);
    }
}
