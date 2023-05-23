using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks_System.Models;

namespace Tasks_System.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int id);
        Task<bool> UserExists(int id);
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> UpdateUser(UserModel user, int idToUpdate);
        Task<bool> DeleteUser(int id);
    }
}
