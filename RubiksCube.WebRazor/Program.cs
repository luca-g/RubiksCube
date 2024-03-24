using RubiksCube.WebRazor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using RubiksCube.Core.Interface;
using RubiksCube.JsonDataProvider.Model;
using RubiksCube.WebRazor.Services;

namespace RubiksCube.WebRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Starting");
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                ConfigureServices(builder, builder.Services);
                // Add services to the container.
                builder.Services.AddControllersWithViews();
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
			catch (Exception ex)
			{
				logger.Error(ex);
			}
			finally
			{
				logger.Info("Exiting program");
				NLog.LogManager.Shutdown();
			}
        }
        public static void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<AppConfiguration>(builder.Configuration.GetSection("RubiksCube"));
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
                loggingBuilder.AddNLog();
            });
            services.AddTransient<IRotationsDataProvider, JsonDataProvider.Model.JsonDataProvider>(
                t =>
                {
                    var configuration = t.GetService<IOptions<AppConfiguration>>();
                    if (configuration?.Value.RotationFile == null)
                    {
                        throw new ArgumentNullException("RotationFile is not set in appsettings.json");
                    }
                    if (!File.Exists(configuration.Value.RotationFile))
                    {
                        throw new ArgumentNullException("RotationFile not found");
                    }
                    return new JsonDataProvider.Model.JsonDataProvider(configuration.Value.RotationFile);
                }
                );
            services.AddTransient<ICubeFactory>(t =>
            {
                var dataProvider = t.GetService<IRotationsDataProvider>();
                return new RubiksCube.Core.Model.CubeFactory(dataProvider!);
            });
            services.AddTransient<CommandService>();
        }
    }
}
