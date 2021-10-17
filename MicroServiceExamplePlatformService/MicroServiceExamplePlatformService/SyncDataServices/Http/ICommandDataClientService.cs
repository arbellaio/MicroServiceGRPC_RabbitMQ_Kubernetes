using System.Threading.Tasks;
using MicroServiceExamplePlatformService.Dtos;

namespace MicroServiceExamplePlatformService.SyncDataServices.Http
{
    public interface ICommandDataClientService
    {
        Task SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}