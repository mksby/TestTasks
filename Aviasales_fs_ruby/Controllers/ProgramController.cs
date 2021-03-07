using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using TestTasks.DTO;
using TestTasks.Interfaces;
using TestTasks.Repositories;

namespace TestTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly ILogger<ProgramController> _logger;
        private readonly IProgramRepository<TestTasks.DTO.Program, int> _programRepository;

        public ProgramController(
            ILogger<ProgramController> logger,
            IProgramRepository<TestTasks.DTO.Program, int> programRepository
        )
        {
            _logger = logger;
            _programRepository = programRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_programRepository.GetAll());
        }
    }
}