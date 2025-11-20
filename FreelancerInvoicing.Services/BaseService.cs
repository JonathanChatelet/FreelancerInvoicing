using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Models.Interfaces;

namespace FreelancerInvoicing.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private IObjectRepository<T> _objectRepository;

        public BaseService(IObjectRepository<T> objectRepository)
        {
            _objectRepository = objectRepository;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsyncService()
        {
            return await _objectRepository.GetAllAsync();
        }

        public virtual async Task<T?> GetObjByIdService(int id)
        {
            return await _objectRepository.GetObjByIdAsync(id);
        }
        public virtual async Task AddObjService(T entity)
        {
            await _objectRepository.AddObjAsync(entity);
        }
        public virtual async Task ModifyObjService(T entity)
        {
            await _objectRepository.ModifyObjAsync(entity);
        }
        public virtual async Task DeletObjService(int id)
        {
            await _objectRepository.DeletObjAsync(id);
        }
    }
}
