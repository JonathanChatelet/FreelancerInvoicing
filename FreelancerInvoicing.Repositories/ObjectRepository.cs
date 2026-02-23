using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FreelancerInvoicing.Models.Entities;
using FreelancerInvoicing.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using System.Linq.Expressions;
using FreelancerInvoicing.Repositories.Context;

namespace FreelancerInvoicing.Repositories
{
    public class ObjectRepository<T> : IObjectRepository<T> where T : class
    {

        protected readonly FreelancerInvoicingDbContext _context;
        protected readonly IMapper _mapper;        
        protected readonly DbSet<T> _dbSet;

        public ObjectRepository(FreelancerInvoicingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllObjectAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetObjectByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddObjectAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyObjectAsync(T entity)
        {
            //_dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletObjectAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
