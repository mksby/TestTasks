using System;
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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository<User, int> _userRepository;
        
        public UserController(
            ILogger<UserController> logger,
            IUserRepository<User, int> userRepository
        )
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<User> Create([FromBody] CreateUser createUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentNullException();
                }

                if (_userRepository.GetByEmail(createUser.Email) != null)
                {
                    _logger.LogWarning($"create user.email {createUser.Email}");
                    
                    return StatusCode(StatusCodes.Status409Conflict);
                }

                var user = _userRepository.Insert(new User()
                {
                    Name = createUser.Name,
                    Email = createUser.Email
                });

                _logger.LogInformation($"create user.id {user.Id}");

                return new JsonResult(user)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception exception)
            {
                _logger.LogError("create user", exception);

                throw;
            }
        }
    }
}