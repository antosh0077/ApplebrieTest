using ApplebrieTest.Datas.Models;
using System.ComponentModel.DataAnnotations;

namespace ApplebrieTest.Common.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
