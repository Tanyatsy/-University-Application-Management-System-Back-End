using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Unipply.Context;
using Microsoft.EntityFrameworkCore;
using Unipply.Repositories;
using Unipply.Services;

namespace Unipply
{
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Unipply", Version = "v1" });
            });

            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<DBContext>(
                opts => opts.UseNpgsql(connectionString)
            );
           
            services.AddScoped<IUserDataRepository, UserDataRepository>();
            services.AddScoped<IFacultyDataRepository, FacultyDataRepository>();
            services.AddScoped<ISpecialtyDataRepository, SpecialtyDataRepository>();

            services.AddScoped <IUserDataService, UserDataService>();
            services.AddScoped <IFacultyDataService, FacultyDataService>();
            services.AddScoped <ISpecialtyDataService, SpecialtyDataService>();
            services.AddScoped <IRecommendationsIteractor, RecommendationsIteractor>();
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unipply v1"));
            }

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
