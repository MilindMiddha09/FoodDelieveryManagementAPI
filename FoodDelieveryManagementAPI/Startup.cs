using FoodDelieveryManagementAPI.Business;
using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace FoodDelieveryManagementAPI
{
    [ExcludeFromCodeCoverage]

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<ApiDbContext>(option => option.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = FoodDelieveryManagementDb;"));
            
            services.AddScoped<IAdminBusiness, AdminBusiness>();
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IRestaurantBusiness, RestaurantBusiness>();
            services.AddScoped<IMenuBusiness, MenuBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<IAdministrationBusiness, AdministrationBusiness>();

            services.AddScoped<IAdminDataRepo, AdminDataRepo>();
            services.AddScoped<ICustomerDataRepo, CustomerDataRepo>();
            services.AddScoped<IRestaurantDataRepo, RestaurantDataRepo>();
            services.AddScoped<IMenuDataRepo, MenuDataRepo>();
            services.AddScoped<IOrderDataRepo, OrderDataRepo>();
            
            services.AddHttpContextAccessor();
            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApiDbContext>();
            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Yum Yum Express",
                    Description = "Food delivered easy.",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
                });

                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
