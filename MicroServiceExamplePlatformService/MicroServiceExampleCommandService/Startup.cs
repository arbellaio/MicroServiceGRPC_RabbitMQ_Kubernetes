using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServiceExampleCommandService.AsyncDataServices;
using MicroServiceExampleCommandService.Data;
using MicroServiceExampleCommandService.EventProcessing;
using MicroServiceExampleCommandService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MicroServiceExampleCommandService
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
            services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("InMem"));
            services.AddScoped<ICommandRepository, CommandRepository>();
            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddScoped<IPlatformDataClient, PlatformDataClient>();
            services.AddControllers();
            services.AddHostedService<MessageBusSubscriber>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroServiceExampleCommandService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroServiceExampleCommandService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            PrepDb.PrepPopulation(app);
        }
    }
}