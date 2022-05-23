using ApplebrieTest.Datas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApplebrieTest.Sercices.Injections
{
    public static class ContextInjection
    {
        public static IServiceCollection _AddDbContext(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(connectionString));
            return services;
        }
    }
}
