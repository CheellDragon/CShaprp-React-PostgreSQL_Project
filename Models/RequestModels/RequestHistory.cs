using TestTaskDotnet.Models.Base;

namespace TestTaskDotnet.Models.RequestModels
{
    public class RequestHistory : EntityBase<int>
    {
        public int RequestID { get; set; }
        public Request? Request { get; set; }
    }
}
