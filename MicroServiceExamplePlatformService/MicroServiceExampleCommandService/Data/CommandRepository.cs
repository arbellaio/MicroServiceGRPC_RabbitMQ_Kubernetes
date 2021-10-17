using System;
using System.Collections.Generic;
using System.Linq;
using MicroServiceExampleCommandService.Models;

namespace MicroServiceExampleCommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            _context.Platforms.Add(platform);
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(x => x.Id.Equals(platformId));
        }

        public bool ExternalPlatformExists(int platformId)
        {
            return _context.Platforms.Any(x => x.ExternalId.Equals(platformId));
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(x => x.PlatformId.Equals(platformId)).ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.FirstOrDefault(x => x.PlatformId.Equals(platformId) && x.Id.Equals(commandId));
        }

        public void CreateCommand(int platformId, Command newCommand)
        {
            if (newCommand == null)
            {
                throw new ArgumentNullException(nameof(newCommand));
            }
            newCommand.PlatformId = platformId;
            _context.Commands.Add(newCommand);

        }
    }
}