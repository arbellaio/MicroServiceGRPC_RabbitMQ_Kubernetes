using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MicroServiceExamplePlatformService.Data;
using MicroServiceExamplePlatformService.Models;
using PlatformService;

namespace MicroServiceExamplePlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext serverCallContext)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();
            foreach (var platform in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
            }

            return Task.FromResult(response);
        }
    }
}