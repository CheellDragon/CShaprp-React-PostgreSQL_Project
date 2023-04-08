using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskDotnet.Interfaces;
using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Controllers
{
    public class RequestController : Controller
    {
        private readonly ILogger<RequestController> _logger;
        private readonly IRequestService _requestService;

        public RequestController(ILogger<RequestController> logger, IRequestService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        public IActionResult MessagePage()
        {
            return View();
        }

        //GET

        [HttpGet, Authorize]
        public IActionResult GetAllRequests()
            => Ok(_requestService.GetAllRequests());

        [HttpGet, Authorize]
        public IActionResult GetMyRequests(int userId)
            => Ok(_requestService.GetMyRequests(userId));

        [HttpGet, Authorize]
        public async Task<IActionResult> GetRequest(int Id)
            => Ok(await _requestService.GetRequest(Id));

        [HttpGet, Authorize]
        public IActionResult GetRequestsHistory()
            => Ok(_requestService.GetRequestsHistory());


        //POST


        [HttpPost, Authorize]
        public async Task<IActionResult> AddRequestToUser(int Id, int UserId)
        {
            var result = await _requestService.AddRequestToUser(Id, UserId);

            return result ? Ok(result) : BadRequest($"Ошибка при добавлении заявки пользователю {UserId}.");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> RemoveRequestFromUser(int Id, int UserId)
        {
            var result = await _requestService.RemoveRequestFromUser(Id, UserId);
            return result ? Ok(result) : BadRequest($"Ошибка при удалении заявки у пользователя {UserId}.");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddRequest(string phoneNumber, string fio, string email, RequestType type)
        {
            var result = await _requestService.AddRequest(phoneNumber, fio, email, type);
            return result ? Ok(result) : BadRequest("Ошибка при создании заявки.");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> RemoveRequest(int Id)
        {
            var result = await _requestService.RemoveRequest(Id);
            return result ? Ok(result) : BadRequest("Ошибка при удалении заявки.");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CancelRequest(int Id)
        {
            var result = await _requestService.CancelRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует.");;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CloseRequest(int Id)
        {
            var result = await _requestService.CloseRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует."); ;
        }
    }
}
