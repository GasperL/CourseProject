using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Hosting;

namespace WebApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseAngular(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = $"{env.WebRootPath}/angular";
                    spa.UseAngularCliServer("start");

                });
            }
            else
            {
                builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("angular"),
                    app =>
                    {
                        app.UseSpa(spaBuilder => spaBuilder.Options.SourcePath = $"{env.WebRootPath}/spa/dist");
                    });
            }
        }
    }
}