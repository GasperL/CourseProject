using Core.ApplicationManagement.Services.CategoryService;
using Core.ApplicationManagement.Services.ManufacturerService;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.ApplicationManagement.Services.ProductService;
using Core.ApplicationManagement.Services.UserService;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Common.Repositories.GenericRepository;
using DataAccess.Entities.Common.Repositories.ProductRepository;
using DataAccess.Entities.Common.Repositories.UserRepository;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void EnableRuntimeCompilation(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserAccountService, UserAccountService>();
            services.AddTransient<IProductGroupService, ProductGroupService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
        }

        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
