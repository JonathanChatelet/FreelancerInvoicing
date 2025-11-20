using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Models.Interfaces
{
    public interface IObjectRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T> GetObjByIdAsync(int id);
        Task AddObjAsync(T entity);
        Task ModifyObjAsync(T entity);
        Task DeletObjAsync(int id);
    }
}
