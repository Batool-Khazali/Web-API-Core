using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_3.Models;

namespace web_api_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("development")]
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
            var products = _db.Products.ToList();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = _db.Products.FirstOrDefault(a => a.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(product);
            }
        }


        [HttpGet("{name:alpha}/{id:int:max(10)}")]
        public IActionResult GetProductByName(string name, int id)
        {
            var x = _db.Products.FirstOrDefault(a => a.ProductName == name && a.ProductId == id);

            if (x == null)
            {
                return NotFound();
            }
            else if (String.IsNullOrEmpty(name) || id <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(x);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var x = _db.Products.FirstOrDefault(a => a.ProductId == id);

            if (x == null)
            {
                return NotFound();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                _db.Products.Remove(x);
                _db.SaveChanges();
                return NoContent();
            }

        }


        [HttpGet("GetProductByCategoryId/{CategoryId:int}")]
        public IActionResult GetProductByCategoryId(int CategoryId)
        {
            var product = _db.Products.Where(a => a.CategoryId == CategoryId).ToList();

            if (product == null)
            {
                return NotFound();
            }
            else if (CategoryId <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(product);
            }
        }

    }
}
