using ApplebrieTest.Sercices.Implementations;
using ApplebrieTest.Sercices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplebrieTest.Sercices.Injections
{
    public static class ServiceInjection
    {
        public static IServiceCollection _AddServiceInjections(this IServiceCollection services)
        {
            
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
