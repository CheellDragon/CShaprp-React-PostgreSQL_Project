using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTaskDotnet.Interfaces;
using TestTaskDotnet.Models.Base;
using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Services
{
    public class RequestService : IRequestService
    {
        private readonly AppDbContext _db;
        public RequestService(AppDbContext db)
            => _db = db;

        //PRIVATE
        private async Task<Request> _getRequestByID(int requestID)
            => await _db.Requests.FirstOrDefaultAsync(r => r.Id == requestID);

        private void _changeRequestStatus(Request request, RequestStatus newStatus)
            => request.Status = newStatus;

        //PUBLIC
        public async Task<bool> CancelRequest(int requestID)
        {
            var request = await _getRequestByID(requestID);
            if (request == null)
                return false;

            _changeRequestStatus(request, RequestStatus.Cancelled);

            _db.Requests.Update(request);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CloseRequest(int requestID)
        {
            var request = await _getRequestByID(requestID);
            if (request == null)
                return false;

            _changeRequestStatus(request, RequestStatus.Closed);

            _db.Requests.Update(request);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Request> GetAllRequests()
            => new List<Request>(_db.Requests);

        public IEnumerable<RequestHistory> GetRequestsHistory()
            => new List<RequestHistory>(_db.RequestsHistory);

        public async Task<Request> GetRequest(int requestID)
            => await _db.Requests.FirstOrDefaultAsync(r => r.Id == requestID);


        public async Task<bool> AddRequestToUser(int requestID, string userName)
        {
            try
            {
                var request = await _getRequestByID(requestID);
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == userName);
                user.Requests = new List<Request>();
                user.Requests.Add(request);

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRequestFromUser(int requestID, string userName)
        {
            try
            {
                var request = await _getRequestByID(requestID);
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == userName);
                user.Requests = await _db.Requests.Where(u => u.Id == request.Id).ToListAsync();
                user.Requests.Remove(request);

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddRequest(string phoneNumber, string fio, string email, RequestType type)
        {
            try
            {
                var newRequest = new Request()
                {
                    PhoneNumber = phoneNumber,
                    FIO = fio,
                    Email = email,
                    Type = type,
                    Status = RequestStatus.Created
                };
                await _db.Requests.AddAsync(newRequest);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRequest(int requestID)
        {
            try
            {
                var request = await _getRequestByID(requestID);
                _db.Requests.Remove(request);
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
