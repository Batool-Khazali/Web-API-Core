using System.ComponentModel.DataAnnotations;

namespace web_api_4.DTOs
{
    public class UsersDTO
    {
        [Required(ErrorMessage = "please enter your user name")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "user name should be between 6 and 50 ")]
        [RegularExpression(@"^([A-Za-z][A-Za-z0-9_]*)$",
        ErrorMessage = "Only alphabets, numbers and _ are allowed. start with letters first.")]
        public string UserName { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "password should be between 8 and 50 ")]
        [DataType(DataType.Password, ErrorMessage = "please enter your password")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^0-9A-Za-z]).*$",
            ErrorMessage ="password must have 1 capital letter, 1 small letter, 1 number and any symbol")]
        public string Password { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
