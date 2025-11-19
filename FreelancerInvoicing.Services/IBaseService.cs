using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerInvoicing.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<T> GetObjByIdService(int id);
        Task AddObjService(T entity);
        Task ModifyObjService(T entity);
        Task DeletObjService(int id);
    }
}
