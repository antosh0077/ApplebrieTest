using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApplebrieTest.Datas.Models
{
    public class AppUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int LoginCount { get; set; }
        public UserType UserType { get; set; }
    }

    
}
