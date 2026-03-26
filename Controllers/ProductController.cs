using Ecommerce.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly AppDbContext _DbContext;

        public ProductController(AppDbContext context)
        {
            _DbContext = context;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(_DbContext.Products.ToList());
        }
            

    }
}
