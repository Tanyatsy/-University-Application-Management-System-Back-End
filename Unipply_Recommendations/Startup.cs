using IndexService.Consumers;
using IndexService.MessageBus;
using IndexService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipply_Recommendations.DataStructures;
using Unipply_Recommendations.Repositories;
using Unipply_Recommendations.Services;

namespace Unipply_Recommendations
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Unipply_Recommendations", Version = "v1" });
            });

            services.AddSingleton<FacultyRecommender, FacultyRecommender>(op => {
                RecommendationDataRepository recommendationDataRepository = new();
                FacultyRecommender recommender = new FacultyRecommender(recommendationDataRepository);
               
                recommender.predictionengine = recommender.LoadData();

                //Initialize the instance here
                return recommender; //Return the instance
            });

            services.AddTransient<FacultyRepository>();
            services.AddTransient<HobbyRepository>();
            services.AddTransient<RecommendationDataRepository>();
            /*var provider = services.BuildServiceProvider();
            var dependency = provider.GetRequiredService<FacultyRepository>();
            Faculty faculty = new(dependency);
            faculty.AddFacultiesMigration();*/

            services.AddHostedService<FacultiesConsumer>();
            services.AddScoped<IMessageBusClient, RabbitMQClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unipply_Recommendations v1"));
            }

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
