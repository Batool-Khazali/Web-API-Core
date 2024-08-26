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
    public class CategoriesController : ControllerBase
    {

        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public IActionResult getAllCategories()
        {
            var all = _db.Categories.ToList();

            if (all != null)
            {
                return Ok(all);
            }
            else
            {
                return NotFound();
            }
        }



        [Route("{id:int:min(5)}")]
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var categoryById = _db.Categories.FirstOrDefault(a => a.CategoryId == id);

            if (categoryById == null)
            {
                return NotFound();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(categoryById);
            }
        }



        [Route("{name:alpha}")]
        [HttpGet]
        public IActionResult GetCategoryByName(string name)
        {
            var categoryByName = _db.Categories.FirstOrDefault(a => a.CategoryName == name);

            if (categoryByName == null)
            {
                return NotFound();
            }
            else if (String.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            else
            {
                return Ok(categoryByName);
            }
        }



        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var categoryToDelete = _db.Categories.FirstOrDefault(a => a.CategoryId == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                _db.Categories.Remove(categoryToDelete);
                _db.SaveChanges();
                return NoContent();
            }

        }


        [HttpPost]
        public IActionResult addProduct([FromForm] CategoriesRequestDto c)
        {
            var newC = new Category
            {
                CategoryName = c.CategoryName,
                CategoryImage = c.CategoryImage.FileName,
            };

            var ImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
            if (!Directory.Exists(ImagesFolder))
            {
                Directory.CreateDirectory(ImagesFolder);
            }
            var imageFile = Path.Combine(ImagesFolder, c.CategoryImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                c.CategoryImage.CopyToAsync(stream);
            }

            if (newC == null)
            {
                return BadRequest();
            }
            else
            {
                _db.Categories.Add(newC);
                _db.SaveChanges();
                return Ok(newC);
            }
        }



        [HttpPut("{id}")]
        public IActionResult updateCategory(int id, [FromForm] CategoriesRequestDto c)
        {

            var catToUpdate = _db.Categories.FirstOrDefault(x => x.CategoryId == id);


            if (catToUpdate == null)
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

                var imageFile = Path.Combine(ImagesFolder, c.CategoryImage.FileName);

                using (var stream = new FileStream(imageFile, FileMode.Create))
                {
                    c.CategoryImage.CopyToAsync(stream);
                }


                catToUpdate.CategoryName = c.CategoryName ?? catToUpdate.CategoryName;
                catToUpdate.CategoryImage = c.CategoryImage.FileName ?? catToUpdate.CategoryImage;

                _db.Categories.Update(catToUpdate);
                _db.SaveChanges();
                return Ok(catToUpdate);
            }

        }

    }
}
