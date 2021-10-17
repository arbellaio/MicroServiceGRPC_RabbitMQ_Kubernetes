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
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepository _repository;

        public PlatformsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine(" ---> Getting platforms from Commands Service");
            var allPlatforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(allPlatforms));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("---> Inbound POST a Command Service");
            return Ok("Inbound test of from Platform Controller");
        }
    }
}