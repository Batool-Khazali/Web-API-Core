using web_api_4.Models;

namespace web_api_4.DTOs
{
    public class CartItemsResponseDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public cartProducts CP {  get; set; }

    }


    public class cartProducts
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public double? Price { get; set; }

        public string? ProductImage { get; set; }


    }



}
