using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsyncService(); 
        Task<T> GetObjByIdService(int id);
        Task AddObjService(T entity);
        Task ModifyObjService(T entity);
        Task DeletObjService(int id);
    }
}
