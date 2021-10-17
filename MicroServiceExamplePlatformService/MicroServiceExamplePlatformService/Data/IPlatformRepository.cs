using System.Collections;
using System.Collections.Generic;
using MicroServiceExamplePlatformService.Models;

namespace MicroServiceExamplePlatformService.Data
{
    public interface IPlatformRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform platform);
    }
}