using ApplebrieTest.Datas.Models;
using System.ComponentModel.DataAnnotations;

namespace ApplebrieTest.Common.DTOs
{
    public class UserDTO
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

        public long LoginCount { get; set; }

        [Required]
        public string UserName { get; set; }

        public UserDTO()
        {

        }
        public UserDTO(AppUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserType = user.UserType;
            UserName = user.UserName;
            LoginCount = user.LoginCount;
        }
    }
}
