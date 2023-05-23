using Microsoft.EntityFrameworkCore;
using Tasks_System.Data;
using Tasks_System.Models;
using Tasks_System.Repository.Interfaces;

namespace Tasks_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TasksDbContext _dbContext;

        public UserRepository (TasksDbContext TaskdbContext)
        {
            _dbContext = TaskdbContext;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UserExists(int id)
        {
            UserModel user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel> UpdateUser(UserModel user, int idToUpdate)
        {
            UserModel userById = await GetUserById(idToUpdate);

            if (userById == null)
            {
                throw new Exception($"User {idToUpdate} not found on database");
            }

            userById.Name = user.Name;
            userById.Mail = user.Mail;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel userById = await GetUserById(id);

            if (userById == null)
            {
                throw new Exception($"User {id} not found on database");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        
    }
}
