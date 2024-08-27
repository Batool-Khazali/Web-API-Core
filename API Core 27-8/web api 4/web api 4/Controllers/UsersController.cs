using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_4.DTOs;
using web_api_4.Models;

namespace web_api_4.Controllers
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


        [HttpPost]
        public IActionResult addUser([FromForm] UserRequestDTO u)
        {
            var newUser = new User
            {
                Username = u.Username,
                Password = u.Password,
                Email = u.Email,
            };

            if (newUser == null)
            {
                return NotFound();
            }
            else
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok(newUser);
            }
        }

        [HttpPut("{id}")]
        public IActionResult updateUserInfo(int id, [FromForm] UserRequestDTO u)
        {

            var user = _db.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                return BadRequest();
            }
            else if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                user.Username = u.Username ?? user.Username;
                user.Password = u.Password ?? user.Password;
                user.Email = u.Email ?? user.Email;

                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok(user);
            }

        }

    }
}
