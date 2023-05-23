using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks_System.Models;
using Tasks_System.Repository.Interfaces;

namespace Tasks_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            try
            {
                List<UserModel> users = await _userRepository.GetAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            try
            {
                UserModel users = await _userRepository.GetUserById(id);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel userToBeCreated)
        {
            try
            {
                UserModel userCreated = await _userRepository.CreateUser(userToBeCreated);

                return Ok(userCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel userToBeUpdated, int id)
        {
            try
            {
                userToBeUpdated.Id = id;
                UserModel user = await _userRepository.UpdateUser(userToBeUpdated, id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            try
            {
                bool deleted = await _userRepository.DeleteUser(id);

                return Ok(deleted);
            }
            catch (Exception)
            {
                return Ok();
            }
        }

    }
}
