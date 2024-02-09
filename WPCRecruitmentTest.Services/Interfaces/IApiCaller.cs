namespace WPCRecruitmentTest.Services.Interfaces
{
    public interface IApiCaller
    {
        public Task<string> SendGetRequestAsync(string url);
    }
}
