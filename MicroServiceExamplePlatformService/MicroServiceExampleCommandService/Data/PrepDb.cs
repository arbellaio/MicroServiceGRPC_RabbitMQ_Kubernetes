using System;
using System.Collections.Generic;
using MicroServiceExampleCommandService.Models;
using MicroServiceExampleCommandService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServiceExampleCommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                if (grpcClient != null)
                {
                    var platforms = grpcClient.ReturnAllPlatform();
                    SeedData(serviceScope.ServiceProvider.GetService<ICommandRepository>(), platforms);
                }
            }
        }

        private static void SeedData(ICommandRepository repository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine($" ---> Seeding new platforms");
            foreach (var platform in platforms)
            {
                if (!repository.ExternalPlatformExists(platform.ExternalId))
                {
                    repository.CreatePlatform(platform);
                }

                repository.SaveChanges();
            }
        }
    }
}