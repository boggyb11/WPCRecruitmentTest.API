
namespace WPCRecruitmentTest.Models
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Error() { }
        public Error(string errorCode, string errorMessage) { 
            Code = errorCode;
            Message = errorMessage;
        }
    }
}
