using TestTaskDotnet.Models.Base;
using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Models.UserModels
{
    public class User : EntityBase<int>
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Request> Requests { get; set; }
    }
}
