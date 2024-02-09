using WPCRecruitmentTest.Models.Enums;

namespace WPCRecruitmentTest.ViewModels.Requests
{
    public class GetCrimesRequest
    {
        public float Lat {  get; set; }
        public float Lng { get; set; }
        public Month Month { get; set; }
    }
}
