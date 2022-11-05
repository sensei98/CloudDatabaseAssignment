using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DAL;
using OnlineStore.Interface;
using OnlineStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(OnlineStore.Startup))]

namespace OnlineStore
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            builder.Services.AddDbContext<OnlineStoreContext>();

           
        }
    }
}
