using MicroServiceExamplePlatformService.Dtos;

namespace MicroServiceExamplePlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishDto platformPublishDto);
    }
}