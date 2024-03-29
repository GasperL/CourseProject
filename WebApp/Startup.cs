using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Extensions;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            
        }

        public IWebHostEnvironment HostEnvironment { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterEntityFramework(Configuration);
            services.RegisterDependencies(Configuration);
            services.RegisterApiServicesDependencies(Configuration);
            services.EnableRuntimeCompilation(HostEnvironment);
            services.RegisterAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy(name: WebApplicationConstants.Cors.PolicyName,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddSpaStaticFiles(configuration =>
            {
                var buildPath = HostEnvironment.IsProduction() ? "/dist" : string.Empty;
                configuration.RootPath = $"{HostEnvironment.WebRootPath}/angular{buildPath}";
            });

            services.AddControllersWithViews();
            services.AddRazorPages()
            .AddRazorRuntimeCompilation();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(WebApplicationConstants.Cors.PolicyName);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.UseAngular(HostEnvironment);
        }
    }
}