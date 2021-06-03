using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using System.Threading.Tasks;

namespace Phoenix.Controllers
{
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _dbContext.Products.ToArrayAsync();

            return Ok(products);
        }

        [HttpGet("firestore")]
        public async Task<IActionResult> GetProductsFromFirestoreAsync()
        {
            var products = await _dbContext.GetProductsFromFirestoreAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromBody] Product product)
        {
            _dbContext.Add(product);

            await _dbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
