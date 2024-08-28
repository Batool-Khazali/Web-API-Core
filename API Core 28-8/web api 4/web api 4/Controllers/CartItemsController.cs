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





    }
}
