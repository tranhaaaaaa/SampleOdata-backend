using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SampleOdata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOdata.Service.Implements
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(NorthWindContext context, IMemoryCache cache, IConfiguration configuration) : base(context, cache, configuration)
        {
        }
        //public Task<IQueryable<Product>> GetSingleAsync(int id)
        //{
        //    return Task.FromResult(_context.Products.Where(x => x.ProductId == id));
        //}
    }
}
