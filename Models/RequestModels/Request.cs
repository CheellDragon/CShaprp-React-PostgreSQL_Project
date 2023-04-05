using TestTaskDotnet.Models.Base;

namespace TestTaskDotnet.Models.RequestModels
{
    public enum RequestType
    {
        Sale,
        Buy,
        Auction
    }

    public enum RequestStatus
    {
        Created,
        Closed,
        Cancelled
    }

    public class Request : EntityBase<int>
    {
        public string? PhoneNumber { get; set; }
        public string? FIO { get; set; }
        public string? Email { get; set; }
        public RequestType Type { get; set; }
        public RequestStatus Status { get; set; }
    }
}
