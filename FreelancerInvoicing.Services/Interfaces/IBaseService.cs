using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllObjectServiceAsync();
        Task<T?> GetObjectByIdServiceAsync(int? id);
        Task<bool> AddObjectServiceAsync(T? entity);
        Task<bool> ModifyObjectServiceAsync(T? entity);
        Task<bool> DeletObjectServiceAsync(int? id);
    }
}
