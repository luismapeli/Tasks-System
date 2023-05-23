using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks_System.Models;

namespace Tasks_System.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllTasks();
        Task<TaskModel> GetTaskById(int id);
        Task<TaskModel> CreateTask(TaskModel user);
        Task<TaskModel> UpdateTask(TaskModel user, int idToUpdate);
        Task<bool> DeleteTask(int id);
    }
}
