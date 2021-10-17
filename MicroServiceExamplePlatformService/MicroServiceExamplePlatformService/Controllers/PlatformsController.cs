using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MicroServiceExamplePlatformService.AsyncDataServices;
using MicroServiceExamplePlatformService.Data;
using MicroServiceExamplePlatformService.Dtos;
using MicroServiceExamplePlatformService.Models;
using MicroServiceExamplePlatformService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExamplePlatformService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClientService _commandDataClientService;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper, ICommandDataClientService commandDataClientService, IMessageBusClient messageBusClient)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
            _commandDataClientService = commandDataClientService;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Getting Platforms");
            var platformItems = _platformRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }
        
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("---> Getting Platform Item");
            var platformItem = _platformRepository.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }
        
        [HttpPost("create", Name = "CreatePlatform")]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            Console.WriteLine("---> Creating Platform Item");
            var newPlatform = _mapper.Map<Platform>(platformCreateDto);
            _platformRepository.CreatePlatform(newPlatform);
            _platformRepository.SaveChanges();
            
            var platformReadDto = _mapper.Map<PlatformReadDto>(newPlatform);
            
            //Send Sync Message
            try
            {
               await _commandDataClientService.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine($" --->  Could not sent synchronously : {e.Message}");
            }

            
            //Send Async Message
            try
            {
                var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishDto.Event = "Platform_Published";
                 _messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CreatedAtRoute("GetPlatformById", new {Id = newPlatform.Id}, newPlatform);
        }
    }
}