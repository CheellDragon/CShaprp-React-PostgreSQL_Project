using Microsoft.EntityFrameworkCore;
using TestTaskDotnet.Interfaces;
using TestTaskDotnet.Models.Base;
using TestTaskDotnet.Models.RequestModels;
using TestTaskDotnet.Models.UserModels;

namespace TestTaskDotnet.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
            => _db = db;

        public async Task<IEnumerable<Request>> GetUserRequests(string userName)
            => (await _db.Users.FirstOrDefaultAsync(u => u.Name == userName)).Requests.ToArray();

        public async Task<bool> Login(string phoneNumber, string password)
            => (await _db.Users.CountAsync(u => u.PhoneNumber == phoneNumber && u.Password == password)) > 0;

        public async Task<bool> RegisterNewUser(string phoneNumber, string userName, string password)
        {
            try
            {
                var newUser = new User()
                {
                    PhoneNumber = phoneNumber,
                    Name = userName,
                    Password = password
                };
                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
