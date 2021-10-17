using System;
using System.Linq;
using MicroServiceExamplePlatformService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServiceExamplePlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder appBuilder, bool isProd)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine(" --->  Attempting to apply migrations ....");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($" ---> Couldn't run migrations {e.Message}");
                }
            }
            
            if (!context.Platforms.Any())
            {
                Console.WriteLine(" ---> Seeding Data");
                context.Platforms.AddRange(
                    new Platform
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform
                    {
                        Name = "Sql Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free"
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine(" --->  We already have data");
            }
        }
    }
}