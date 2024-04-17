using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOdata.Service
{
    public  interface IService<T> : IDisposable, IAsyncDisposable where T : class
    {
        IQueryable<T> GetAll();

        ValueTask<T> GetObjectAsync(int id);

        Task<int> CreateAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);
    }
}
