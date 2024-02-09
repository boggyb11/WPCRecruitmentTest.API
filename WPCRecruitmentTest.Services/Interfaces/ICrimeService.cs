using WPCRecruitmentTest.Models;
using WPCRecruitmentTest.ViewModels.Requests;
using WPCRecruitmentTest.ViewModels.Responses;

namespace WPCRecruitmentTest.Services.Interfaces
{
    public interface ICrimeService
    {
        Task<CommandResult<CrimeViewModel>> GetCrimes(GetCrimesRequest request);
    }
}
