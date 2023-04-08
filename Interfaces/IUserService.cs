using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(string phoneNumber, string password);
        Task<int> FindIdByPhone(string phoneNumber);
        Task<string> FindNameByPhone(string phoneNumber);
        Task<bool> RegisterNewUser(string phoneNumber, string userName, string password);
        Task<IEnumerable<Request>> GetUserRequests(string userName);
        string CreateToken(string UserName, IConfiguration _configuration);
    }
}
