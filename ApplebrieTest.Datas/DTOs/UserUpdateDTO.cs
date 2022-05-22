using ApplebrieTest.Datas.Models;
using System.ComponentModel.DataAnnotations;

namespace ApplebrieTest.Datas.DTOs
{
    public class UserUpdateDTO
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserType UserType { get; set; }        

        [Required]
        public string UserName { get; set; }

        
    }
}
