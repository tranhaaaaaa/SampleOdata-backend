using SampleOdata.Models;
using SampleOdata.Service;

namespace SampleOdata.WebApi.Controllers
{
    public class ProductController : BaseControllers<Product>
    {
        public ProductController(IService<Product> service) : base(service)
        {
        }
    }
}
