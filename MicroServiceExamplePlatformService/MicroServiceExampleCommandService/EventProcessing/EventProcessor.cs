using System;
using System.Text.Json.Serialization;
using AutoMapper;
using MicroServiceExampleCommandService.Data;
using MicroServiceExampleCommandService.Dtos;
using MicroServiceExampleCommandService.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MicroServiceExampleCommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    //Todo
                    Console.WriteLine();
                    break;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetService<ICommandRepository>();
                var platformPublishedDto = JsonConvert.DeserializeObject<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformExists(platform.ExternalId))
                    {
                        repo.CreatePlatform(platform);
                        repo.SaveChanges();
                        Console.WriteLine($" ---> Platform added");

                    }
                    else
                    {
                        Console.WriteLine($" ---> Platform already exists");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($" ---> Could not add platform to db {e.Message}");
                    Console.WriteLine(e);
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine($" ---> Determining Event");
            var eventType = JsonConvert.DeserializeObject<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine($" ---> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine($" ---> Could not determine Event Type");
                    return EventType.Undetermined;
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }

    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}