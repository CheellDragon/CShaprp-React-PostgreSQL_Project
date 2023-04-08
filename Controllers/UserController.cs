using Microsoft.AspNetCore.Mvc;
using TestTaskDotnet.Interfaces;

namespace TestTaskDotnet.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        //GET

        [HttpGet]
        public async Task<IActionResult> Login(string phoneNumber, string password)
        {
            var user = await _userService.Login(phoneNumber, password);
            if (user == false)
            {
                return BadRequest("Invalid phone number or password");
            }
            var UserId = await _userService.FindIdByPhone(phoneNumber);
            var UserName = await _userService.FindNameByPhone(phoneNumber);
            var Token = _userService.CreateToken(UserName, _configuration);
            return Ok(new { PhoneNumber = phoneNumber, ID = UserId, fio = UserName, token = Token });
        }


        [HttpGet]
        public async Task<IActionResult> GetUserRequests(string userName)
           => Ok( await _userService.GetUserRequests(userName) );


        //POST

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(string phoneNumber, string name, string password)
        {
            //для вывода сообщения на страницу
            var result = await _userService.RegisterNewUser(phoneNumber, name, password);
            //ViewBag.Message = result ? "Регистрация прошла успешно" : "Ошибка при регистрации пользователя";
            //return View();
            //если работать напрямую с запросами
            return result ? Ok("Вы успешно зарегистрировались") : BadRequest($"Ошибка при регистрации пользователя.");
        }
    }
}
