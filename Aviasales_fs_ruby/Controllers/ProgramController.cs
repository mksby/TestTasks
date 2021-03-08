using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly ISubscriptionRepository<Subscription, int> _subscriptionRepository;

        public ProgramController(
            ILogger<ProgramController> logger,
            IProgramRepository<TestTasks.DTO.Program, int> programRepository,
            ISubscriptionRepository<Subscription, int> subscriptionRepository
        )
        {
            _logger = logger;
            _programRepository = programRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<TestTasks.DTO.Program>> Get()
        {
            return new JsonResult(_programRepository.GetAll());
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{term}")]
        public ActionResult<List<TestTasks.DTO.Program>> GetAutocomplete(string term)
        {
            var programs = _programRepository.GetByTerm(term)
                    .ToList()
                    .OrderByDescending(program => _subscriptionRepository.GetByProgramId(program.Id).Count());

            return new JsonResult(programs);
        }
    }
}