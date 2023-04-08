using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public async Task<int> FindIdByPhone(string phoneNumber)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return (user.Id);
        }
        public async Task<string> FindNameByPhone(string phoneNumber)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return (user.Name);
        }

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

        public string CreateToken(string UserName, IConfiguration _configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
