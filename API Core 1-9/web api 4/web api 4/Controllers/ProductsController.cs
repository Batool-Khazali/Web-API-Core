using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_4.DTOs;
using web_api_4.Models;

namespace web_api_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("development")]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(MyDbContext db, ILogger<ProductsController> logger)
        {
            _db = db;
            _logger = logger;
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


        [HttpGet("orderbyprice")]
        public IActionResult orderByPrice()
        {
            var order = _db.Products.OrderByDescending(a => a.Price);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(order);
            }
        }



        [HttpPost]
        public IActionResult addProduct([FromForm] ProductsRequestDTO p)
        {
            var newP = new Product
            {
                ProductName = p.ProductName,
                ProductImage = p.ProductImage.FileName,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
            };

            var ImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
            if (!Directory.Exists(ImagesFolder))
            {
                Directory.CreateDirectory(ImagesFolder);
            }
            var imageFile = Path.Combine(ImagesFolder, p.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                p.ProductImage.CopyToAsync(stream);
            }

            if (newP == null)
            {
                return BadRequest();
            }
            else
            {
                _db.Products.Add(newP);
                _db.SaveChanges();
                return Ok(newP);
            }
        }



        [HttpPut("{id}")]
        public IActionResult updateProducts(int id, [FromForm] ProductsRequestDTO p)
        {

            var proToUpdate = _db.Products.FirstOrDefault(x => x.ProductId == id);


            if (proToUpdate == null)
            {
                return NotFound();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {

                var ImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
                if (!Directory.Exists(ImagesFolder))
                {
                    Directory.CreateDirectory(ImagesFolder);
                }

                var imageFile = Path.Combine(ImagesFolder, p.ProductImage.FileName);

                using (var stream = new FileStream(imageFile, FileMode.Create))
                {
                    p.ProductImage.CopyToAsync(stream);
                }


                proToUpdate.ProductName = p.ProductName ?? proToUpdate.ProductName;
                proToUpdate.ProductImage = p.ProductImage.FileName ?? proToUpdate.ProductImage;
                proToUpdate.Price = p.Price ?? proToUpdate.Price;
                proToUpdate.Description = p.Description ?? proToUpdate.Description;
                proToUpdate.CategoryId = p.CategoryId ?? proToUpdate.CategoryId;

                _db.Products.Update(proToUpdate);
                _db.SaveChanges();
                return Ok(proToUpdate);
            }

        }



    }
}
