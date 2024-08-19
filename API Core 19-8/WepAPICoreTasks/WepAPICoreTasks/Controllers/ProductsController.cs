using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult getAllProducts()
        {

            var productsList = _db.Products.Include(p => p.Category).ToList();


            // DTO method

            //var productsList = _db.Products
            //          .Select(p => new
            //          {

            //              p.ProductName,
            //              p.Description,
            //              p.Price,
            //              CategoryName = p.Category.CategoryName,
            //              p.ProductImage
            //          })
            //          .ToList();

            return Ok(productsList);
        }


        [HttpGet("{ProductID}")]
        public IActionResult GetProductById(int ProductID)
        {
            var productById = _db.Products.FirstOrDefault(a => a.ProductId == ProductID);

            return Ok(productById);

        }



    }
}
