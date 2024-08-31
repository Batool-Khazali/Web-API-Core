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

        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(MyDbContext db, ILogger<CategoriesController> logger)
        {
            _db = db;
            _logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult getAllCategories()
        {
            var all = _db.Categories.ToList();

            if (all != null)
            {
                _logger.LogInformation("success yay");
                return Ok(all);
            }
            else
            {
                _logger.LogInformation("failed wawa");
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
                _db.Categories.RemoveRange(categoryToDelete);
                _db.SaveChanges();
                return NoContent();
            }

        }


        [HttpPost("add")]
        public IActionResult addCategory([FromForm] CategoriesRequestDto c)
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


        //// Helper method to upload images
        //private async Task<string> UploadImageAsync(CategoriesRequestDto dto) //alter this depending on your DTO
        //{
        //    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "images"); // Images is the name of the Imagse folder 

        //    if (!Directory.Exists(uploadFolder))
        //    {
        //        Directory.CreateDirectory(uploadFolder);
        //    }

        //    var imageFile = Path.Combine(uploadFolder, dto.CategoryImage.FileName); // here we get the file name to combine it with the folder path

        //    using (var stream = new FileStream(imageFile, FileMode.Create))
        //    {
        //        await dto.CategoryImage.CopyToAsync(stream);
        //    }

        //    // Return the full URL to the image
        //    var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{dto.CategoryImage.FileName}"; // this is the full path from the server , you can use it in the front end 
        //    return imageUrl; //this method returns the full url of the image , store this in the table as a image path , then use it in your front end 
        //}



        //[HttpPost("add")]
        //public async Task<ActionResult> addCategory([FromForm] CategoriesRequestDto c)
        //{
        //    var imageName = await UploadImageAsync(c); //here we call the methed that we made before , pass ur DTO object here

        //    var newC = new Category
        //    {
        //        CategoryName = c.CategoryName,
        //        CategoryImage = imageName,
        //    };

        //    if (newC == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        _db.Categories.Add(newC);
        //        await _db.SaveChangesAsync();
        //        return Ok(newC);
        //    }
        //}


        //[HttpPost("add")]
        //public async Task<ActionResult> addCategory([FromForm] CategoriesRequestDto c)
        //{


        //    var ImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
        //    if (!Directory.Exists(ImagesFolder))
        //    {
        //        Directory.CreateDirectory(ImagesFolder);
        //    }
        //    var imageFile = Path.Combine(ImagesFolder, c.CategoryImage.FileName);
        //    using (var stream = new FileStream(imageFile, FileMode.Create))
        //    {
        //        await c.CategoryImage.CopyToAsync(stream);
        //    }

        //    var newC = new Category
        //    {
        //        CategoryName = c.CategoryName,
        //        CategoryImage = c.CategoryImage.FileName,
        //    };

        //    if (newC == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        _db.Categories.Add(newC);
        //        _db.SaveChanges();
        //        return Ok(newC);
        //    }
        //}



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
