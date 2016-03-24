using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OdeToFood.Services;
using Microsoft.AspNet.Routing;
using System;
using OdeToFood.Entities;
using Microsoft.Data.Entity;

namespace OdeToFood
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration["database:connection"]));

            // AddSingleton - the lambda overload is a way of stating for any provider return the Configuration instance 
            services.AddSingleton(provider => Configuration);
            // AddSingleton - whereever the app finds an instance of IGreeter, replace it with an instance of the Greeter class.
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment environment,
            IGreeter greeter)
        {
            // This piece of middleware allows the use of windows authentication
            app.UseIISPlatformHandler();

            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRuntimeInfoPage();
            // Note that the order of middleware is important as below
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            // OR!
            // Just use this, which by default has them in order
            app.UseFileServer();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {                
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
        }



        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
