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
using TestTasks.ViewModels;

namespace TestTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly ILogger<ProgramController> _logger;
        private readonly IProgramRepository<TestTasks.DTO.Program, int> _programRepository;
        private readonly IProgramBanRepository<ProgramBan, int> _programBanRepository;
        private readonly ISubscriptionRepository<Subscription, int> _subscriptionRepository;
        private readonly IUserRepository<User, int> _userRepository;

        public ProgramController(
            ILogger<ProgramController> logger,
            IProgramRepository<TestTasks.DTO.Program, int> programRepository,
            IProgramBanRepository<ProgramBan, int> programBanRepository,
            ISubscriptionRepository<Subscription, int> subscriptionRepository,
            IUserRepository<User, int> userRepository
        )
        {
            _logger = logger;
            _programRepository = programRepository;
            _programBanRepository = programBanRepository;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
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
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        [Route("ban")]
        public ActionResult<User> Ban([FromBody] ProgramUserBan programUserBan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentNullException();
                }
                
                var user = _userRepository.GetById(programUserBan.UserId);
                
                if (user == null)
                {
                    _logger.LogWarning($"user ban {programUserBan.UserId}");
                    
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                var program = _programRepository.GetById(programUserBan.ProgramId);
                
                if (program == null)
                {
                    _logger.LogWarning($"user program ban {programUserBan.ProgramId}");
                    
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                var programUserBanStore =
                    _programBanRepository.GetByUserProgramIds(programUserBan.UserId, programUserBan.ProgramId);

                if (programUserBan.Ban)
                {
                    if (programUserBanStore == null)
                    {
                        var ban = _programBanRepository.Insert(new ProgramBan()
                        {
                            UserId = programUserBan.UserId,
                            ProgramId = programUserBan.ProgramId
                        });

                        _logger.LogInformation($"user banned {user.Id}");
                
                        return new JsonResult(ban);
                    }
                    else
                    {
                        _logger.LogWarning($"user program already banned {programUserBan.UserId} {programUserBan.ProgramId}");
                        
                        return StatusCode(StatusCodes.Status409Conflict);
                    }
                }
                else
                {
                    if (programUserBanStore == null)
                    {
                        _logger.LogWarning($"user program already unbanned {programUserBan.UserId} {programUserBan.ProgramId}");
                        
                        return StatusCode(StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        _programBanRepository.Delete(programUserBanStore.Id);

                        _logger.LogInformation($"user unbanned {user.Id}");

                        return Ok();
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("user ban", exception);

                throw;
            }
        }
    }
}