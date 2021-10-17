using System.Collections;
using System.Collections.Generic;
using MicroServiceExampleCommandService.Models;

namespace MicroServiceExampleCommandService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();
        
        // Platform related methods
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int platformId);

        //Command related methods
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command newCommand);
    }
}