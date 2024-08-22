using API_Core_21_8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Core_21_8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public IActionResult getAllUsers()
        {
            var all = _db.Users.ToList();

            if (all != null)
            {
                return Ok(all);
            }
            else
            {
                return NotFound();
            }
        }



        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var x = _db.Users.FirstOrDefault(a => a.UserId == id);

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
                return Ok(x);
            }
        }



        [Route("{name:alpha}")]
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            var x = _db.Users.FirstOrDefault(a => a.Username == name);

            if (x == null)
            {
                return NotFound();
            }
            else if (String.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            else
            {
                return Ok(x);
            }
        }



        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var x = _db.Users.FirstOrDefault(a => a.UserId == id);

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
                _db.Users.Remove(x);
                _db.SaveChanges();
                return NoContent();
            }

        }

    }
}
