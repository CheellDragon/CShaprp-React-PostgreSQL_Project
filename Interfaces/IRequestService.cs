﻿using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<Request> GetAllRequests();
        IEnumerable<Request> GetMyRequests(int userId); 
        Task<Request> GetRequest(int requestID);
        Task<bool> AddRequest(string phoneNumber, string fio, string email, RequestType type);
        Task<bool> AddRequestToUser(int requestID, int id);
        Task<bool> RemoveRequestFromUser(int requestID, int UserId);
        Task<bool> RemoveRequest(int requestID);
        Task<bool> CancelRequest(int requestID);
        Task<bool> CloseRequest(int requestID);
        IEnumerable<RequestHistory> GetRequestsHistory();
    }
}
