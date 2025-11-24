using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories.Interfaces;

namespace FreelancerInvoicing.Repositories
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private IObjectRepository<T> _objectRepository;

        public BaseService(IObjectRepository<T> objectRepository)
        {
            _objectRepository = objectRepository;
        }
        public virtual async Task<IEnumerable<T>> GetAllObjectServiceAsync()
        {
            return await _objectRepository.GetAllObjectAsync();
        }

        public virtual async Task<T?> GetObjectByIdServiceAsync(int id)
        {
            return await _objectRepository.GetObjectByIdAsync(id);
        }
        public virtual async Task AddObjectServiceAsync(T entity)
        {
            await _objectRepository.AddObjectAsync(entity);
        }
        public virtual async Task ModifyObjectServiceAsync(T entity)
        {
            await _objectRepository.ModifyObjectAsync(entity);
        }
        public virtual async Task DeletObjectServiceAsync(int id)
        {
            await _objectRepository.DeletObjectAsync(id);
        }
    }
}
