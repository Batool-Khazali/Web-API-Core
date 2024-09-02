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
    public class CartItemsController : ControllerBase
    {

        private readonly MyDbContext _db;

        public CartItemsController(MyDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult allCartItems()
        {
            var cartItems = _db.CartItems.Select(x =>
            new CartItemsResponseDTO
            {
                CartItemId = x.CartItemId,
                CartId = x.CartId,
                Quantity = x.Quantity,
                CP = new cartProducts
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price,
                    ProductImage = x.Product.ProductImage,
                }
            }
            ).ToList();

            return Ok(cartItems);
        }


        [HttpPost]
        public IActionResult addProductToCart([FromBody] addToCartDTO a)
        {
            var data = new CartItem
            {
                CartId = a.CartId,
                Quantity = a.Quantity,
                ProductId = a.ProductId,
            };

            _db.CartItems.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }


        [HttpDelete("{CartItemId}")]
        public IActionResult DeleteCartItem(int CartItemId)
        {
            var x = _db.CartItems.FirstOrDefault(a => a.CartItemId == CartItemId);

            if (x == null)
            {
                return NotFound();
            }
            else if (CartItemId <= 0)
            {
                return BadRequest();
            }
            else
            {
                _db.CartItems.RemoveRange(x);
                _db.SaveChanges();
                return NoContent();
            }

        }


        [HttpPut("{CartItemId}")]
        public IActionResult updateCartItem(int CartItemId, [FromBody] updateCartItemDTO upCart)
        {
            var ci = _db.CartItems.FirstOrDefault(a => a.CartItemId == CartItemId);

            if (ci == null)
            {
                return NotFound($"no cart with the id of {CartItemId}");
            }
            else
            {
                ci.Quantity = upCart.Quantity;

                _db.CartItems.Update(ci);
                _db.SaveChanges();
                return Ok(ci);
            }
        }







    }
}
