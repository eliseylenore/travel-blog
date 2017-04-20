using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TravelBlog.Models;
using System;

namespace TravelBlog
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //for testing later, can set up a separate in-memory context(NOT TravelBlogContext)
            //services.AddDbContext<TravelBlogContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();

            services.AddEntityFramework()
                .AddDbContext<TravelBlogContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }


        public void Configure(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<TravelBlogContext>();
            AddTestData(context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Places}/{action=index}/{id?}");
            });

            app.Run(async (context1) =>
            {
                await context1.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddTestData(TravelBlogContext context)
        {
            var testUser1 = new Models.Person
            {
                Name = "Elise"

            };

            context.People.Add(testUser1);

            var testExperience = new Models.Experience
            {
                Name = "Go to the beach",
                Story = "It was awesome",
                PersonId = 1,
                PlaceId = 1
            };

            context.Experiences.Add(testExperience);

            var testLocation = new Models.Place
            {
                Name = "Los Angeles"
            };

            context.Places.Add(testLocation);

            context.SaveChanges();
        }
    }
}
