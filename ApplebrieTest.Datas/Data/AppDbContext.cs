using ApplebrieTest.Datas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplebrieTest.Datas.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppUserRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }        
    }
}
