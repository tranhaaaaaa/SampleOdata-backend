using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleOdata.Models;

namespace SampleOdata.Service
{
    public class BaseService<T> : IService<T> where T : class
    {
        public readonly NorthWindContext _context;
        public BaseService(NorthWindContext context, IMemoryCache cache, IConfiguration configuration)
        {
            _context = context;
        }
        public virtual async Task<int> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public virtual Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            var result = _context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public ValueTask<T> GetObjectAsync(int id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public virtual Task<int> UpdateAsync(T entity)
        {
            if (entity == null)
            _context.Entry(entity).State = EntityState.Modified;
            var result = _context.SaveChangesAsync();
            return result;
        }


    }

}
