namespace web_api_4.DTOs
{
    public class CategoriesRequestDto
    {
        public string? CategoryName { get; set; }

        public IFormFile? CategoryImage { get; set; }
    }
}
