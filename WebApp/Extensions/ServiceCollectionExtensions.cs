using Core.ApplicationManagement.Services.ApiServices;
using Core.ApplicationManagement.Services.CartService;
using Core.ApplicationManagement.Services.CategoryService;
using Core.ApplicationManagement.Services.ManufacturerService;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.ApplicationManagement.Services.ProductService;
using Core.ApplicationManagement.Services.ProviderService;
using Core.ApplicationManagement.Services.UserService;
using Core.Mappings;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Common.Repositories.GenericRepository;
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

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IUserAccountService, UserAccountService>();
            services.AddTransient<IProductGroupService, ProductGroupService>();
            services.AddTransient<IProductApiService, ProductApiService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IProviderService, ProviderService>();
        }

        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
