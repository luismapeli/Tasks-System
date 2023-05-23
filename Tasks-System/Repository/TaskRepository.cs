using Microsoft.EntityFrameworkCore;
using Tasks_System.Data;
using Tasks_System.Models;
using Tasks_System.Repository.Interfaces;

namespace Tasks_System.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksDbContext _dbContext;
        private readonly IUserRepository _userRepository;

        public TaskRepository
        (
        TasksDbContext TaskdbContext,
        IUserRepository UserRepository
        )
        {
            _dbContext = TaskdbContext;
            _userRepository = UserRepository;
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            if (task.UserId != null) 
            {
                int userId = (int)task.UserId;
                bool userExists = await _userRepository.UserExists(userId);
                if (userExists == false)
                {
                    throw new Exception($"User {userId} not found on database");
                }
            }
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task, int idToUpdate)
        {
            TaskModel taskById = await GetTaskById(idToUpdate);

            if (taskById == null)
            {
                throw new Exception($"Task {idToUpdate} not found on database");
            }

            taskById.Name = task.Name;
            taskById.Status = task.Status;
            taskById.Description = task.Description;
            taskById.User = task.User;
            taskById.UserId = task.UserId;

            _dbContext.Tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> DeleteTask(int id)
        {
            TaskModel taskById = await GetTaskById(id);

            if (taskById == null)
            {
                throw new Exception($"Task {id} not find on database");
            }

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        
    }
}
