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

        [HttpGet]
        public IActionResult GetAllRequests()
            => Ok(_requestService.GetAllRequests());

        [HttpGet]
        public async Task<IActionResult> GetRequest(int Id)
            => Ok(await _requestService.GetRequest(Id));

        [HttpGet]
        public IActionResult GetRequestsHistory()
            => Ok(_requestService.GetRequestsHistory());


        //POST


        [HttpPost]
        public async Task<IActionResult> AddRequestToUser(int Id, string Name)
        {
            var result = await _requestService.AddRequestToUser(Id, Name);

            return result ? Ok(result) : BadRequest($"Ошибка при добавлении заявки пользователю {Name}.");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequestFromUser(int Id, string Name)
        {
            var result = await _requestService.RemoveRequestFromUser(Id, Name);
            return result ? Ok(result) : BadRequest($"Ошибка при удалении заявки у пользователя {Name}.");
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(string phoneNumber, string fio, string email, RequestType type)
        {
            var result = await _requestService.AddRequest(phoneNumber, fio, email, type);
            return result ? Ok(result) : BadRequest("Ошибка при создании заявки.");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequest(int Id)
        {
            var result = await _requestService.RemoveRequest(Id);
            return result ? Ok(result) : BadRequest("Ошибка при удалении заявки.");
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(int Id)
        {
            var result = await _requestService.CancelRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует.");;
        }

        [HttpPost]
        public async Task<IActionResult> CloseRequest(int Id)
        {
            var result = await _requestService.CloseRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует."); ;
        }
    }
}
