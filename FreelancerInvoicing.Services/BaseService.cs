using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Services.Interfaces;
using FreelancerInvoicing.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IObjectRepository<T> _objectRepository;

        public BaseService(IObjectRepository<T> objectRepository)
        {
            _objectRepository = objectRepository;
        }
        public virtual async Task<IEnumerable<T>> GetAllObjectServiceAsync()
        {
            return await _objectRepository.GetAllObjectAsync();
        }

        public virtual async Task<T?> GetObjectByIdServiceAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _objectRepository.GetObjectByIdAsync(id.Value);
        }
        public virtual async Task<bool> AddObjectServiceAsync(T? entity)
        {
            if (entity == null)
            {
                return false;
            }
            await _objectRepository.AddObjectAsync(entity);
            return true;
        }
        public virtual async Task<bool> ModifyObjectServiceAsync(T? entity)
        {
            if (entity == null)
            {
                return false;
            }
            await _objectRepository.ModifyObjectAsync(entity);
            return true;
        }
        public virtual async Task<bool> DeletObjectServiceAsync(int? id)
        {
            T? entity = await GetObjectByIdServiceAsync(id);
            if (entity == null)
            {
                return false;
            }
            await _objectRepository.DeletObjectAsync(entity);
            return true;
        }
    }
}
