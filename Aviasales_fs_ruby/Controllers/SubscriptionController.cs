using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTasks.DTO;
using TestTasks.Interfaces;
using TestTasks.ViewModels;

namespace TestTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriptionRepository<Subscription, int> _subscriptionRepository;
        private readonly IUserRepository<User, int> _userRepository;
        private readonly IProgramRepository<TestTasks.DTO.Program, int> _programRepository;
        private readonly IProgramBanRepository<ProgramBan, int> _programBanRepository;

        public SubscriptionController(
            ILogger<SubscriptionController> logger,
            ISubscriptionRepository<Subscription, int> subscriptionRepository,
            IUserRepository<User, int> userRepository,
            IProgramRepository<TestTasks.DTO.Program, int> programRepository,
            IProgramBanRepository<ProgramBan, int> programBanRepository
        )
        {
            _logger = logger;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _programRepository = programRepository;
            _programBanRepository = programBanRepository;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public ActionResult<List<Subscription>> Create([FromBody] CreateSubscription createSubscription)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentNullException();
                }

                if (_userRepository.GetById(createSubscription.UserId) == null)
                {
                    _logger.LogWarning($"create subscription {createSubscription.UserId}");

                    return StatusCode(StatusCodes.Status404NotFound);
                }
                
                if (_programRepository.GetById(createSubscription.ProgramId) == null)
                {
                    _logger.LogWarning($"create subscription {createSubscription.ProgramId}");

                    return StatusCode(StatusCodes.Status404NotFound);
                }

                if (_subscriptionRepository.GetByUserProgramIds(createSubscription.UserId, createSubscription.ProgramId) != null)
                {
                    _logger.LogWarning($"create subscription {createSubscription.UserId} {createSubscription.ProgramId}");

                    return StatusCode(StatusCodes.Status409Conflict);
                }

                var subscription = _subscriptionRepository.Insert(new Subscription()
                {
                    UserId = createSubscription.UserId,
                    ProgramId = createSubscription.ProgramId
                });

                _logger.LogInformation($"create subscription.id {subscription.Id}");

                return new JsonResult(subscription)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception exception)
            {
                _logger.LogError("create subscription", exception);

                throw;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("{userId}")]
        public ActionResult<List<TestTasks.DTO.Program>> GetPrograms(int userId)
        {
            try
            {
                if (_userRepository.GetById(userId) == null)
                {
                    _logger.LogWarning($"get subscription programs {userId}");

                    return StatusCode(StatusCodes.Status404NotFound);
                }

                var banPrograms = _programBanRepository.GetUserPrograms(userId)
                    .Select(p => p.ProgramId);

                var programsIds = _subscriptionRepository.GetByUserId(userId)
                    .Select(s => s.ProgramId)
                    .Except(banPrograms)
                    .ToArray();

                var programs = _programRepository.GetByIds(programsIds);
                
                _logger.LogInformation($"get subscription programs {string.Join(", ", programsIds)}");

                return new JsonResult(programs);
            }
            catch (Exception exception)
            {
                _logger.LogError("get subscription programs", exception);

                throw;
            }
        }
    }
}