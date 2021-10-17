using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using MicroServiceExampleCommandService.Data;
using MicroServiceExampleCommandService.Dtos;
using MicroServiceExampleCommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExampleCommandService.Controllers
{
    [Produces("application/json")]
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepository _repository;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($" ---> Hit GetCommandsForPlatform: {platformId}");
            if (!_repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }
    }
}