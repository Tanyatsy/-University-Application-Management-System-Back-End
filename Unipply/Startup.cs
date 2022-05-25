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
using RankService.MessageBus;
using Unipply.Consumers;

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
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Unipply", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<DBContext>(
                opts => {
                    opts.UseNpgsql(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    opts.EnableSensitiveDataLogging();
                        }
            );
           
            services.AddScoped<IUserDataRepository, UserDataRepository>();
            services.AddScoped<IFacultyDataRepository, FacultyDataRepository>();
            services.AddScoped<ISpecialtyDataRepository, SpecialtyDataRepository>();
            services.AddScoped<IUserProfileDataRepository, UserProfileDataRepository>();
            services.AddScoped<IFacultyDataUserProfileDataRepository, FacultyDataUserProfileDataRepository>();
            services.AddScoped<ISpecialtyDataUserProfileDataRepository, SpecialtyDataUserProfileDataRepository>();

            services.AddScoped <IUserDataService, UserDataService>();
            services.AddScoped <IFacultyDataService, FacultyDataService>();
            services.AddScoped <ISpecialtyDataService, SpecialtyDataService>();
            services.AddScoped <IUserProfileDataService, UserProfileDataService>();

            services.AddScoped<IRecommendationsIteractor, RecommendationsIteractor>();
            services.AddScoped<IRecommendationsService, RecommendationsService>();
            services.AddHostedService<RecommendationFacultiesConsumer>();
            services.AddHostedService<RecommendationHobbiesConsumer>();
            services.AddScoped<IMessageBusClient, RabbitMQClient>();
            services.AddScoped<IEmailSender, EmailSender>();
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
