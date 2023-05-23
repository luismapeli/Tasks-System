using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks_System.Models;
using Tasks_System.Repository.Interfaces;

namespace Tasks_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAllTasks()
        {

            try
            {
                List<TaskModel> tasks = await _taskRepository.GetAllTasks();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetTaskById(int id)
        {
            try
            {
                TaskModel task = await _taskRepository.GetTaskById(id);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel taskToBeCreated)
        {
            try
            {
                TaskModel taskCreated = await _taskRepository.CreateTask(taskToBeCreated);

                return Ok(taskCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel taskToBeUpdated, int id)
        {
            try
            {
                taskToBeUpdated.Id = id;
                TaskModel user = await _taskRepository.UpdateTask(taskToBeUpdated, id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> DeleteTask(int id)
        {
            try
            {
                bool deleted = await _taskRepository.DeleteTask(id);

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(204);
            }
        }


    }
}
