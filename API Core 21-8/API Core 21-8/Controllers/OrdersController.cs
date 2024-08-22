using API_Core_21_8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Core_21_8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly MyDbContext _db;

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public IActionResult getAllOrders()
        {
            var x = _db.Orders.ToList();

            if (x != null)
            {
                return Ok(x);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOrderById(int id)
        {
            var x = _db.Orders.FirstOrDefault(a => a.OrderId == id);

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


        ////////////// there's no column for product name
        ////////////// so there's no api for GetOrderByName



        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var x = _db.Orders.FirstOrDefault(a => a.OrderId == id);

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
                _db.Orders.Remove(x);
                _db.SaveChanges();
                return NoContent();
            }

        }

    }
}
