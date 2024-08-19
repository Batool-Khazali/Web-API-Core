using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var categories = _db.Categories.ToList();

            return Ok(categories);
        }


        [HttpGet("{CategoryID}")]
        public IActionResult GetCategoryById(int CategoryID)
        {
            var categoryById = _db.Categories.FirstOrDefault(a => a.CategoryId == CategoryID);

            return Ok(new { categoryById });
        }

    }
}
