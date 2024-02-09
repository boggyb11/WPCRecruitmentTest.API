using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WPCRecruitmentTest.Models;
using WPCRecruitmentTest.Services.Interfaces;
using WPCRecruitmentTest.Services.Services;
using WPCRecruitmentTest.ViewModels.Requests;
using WPCRecruitmentTest.ViewModels.Responses;

namespace WPCRecruitmentTest.Services.Services
{
    public class CrimeService(IConfiguration configuration, DateHelper dateHelper, IApiCaller apiCaller, LocationHelper locationHelper) : ICrimeService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly DateHelper _dateHelper = dateHelper;
        private readonly IApiCaller _apiCaller = apiCaller;
        private readonly LocationHelper _locationHelper = locationHelper;

        public async Task<CommandResult<CrimeViewModel>> GetCrimes(GetCrimesRequest request)
        {
            try
            {
                if (!locationHelper.IsWithinUk(request.Lat, request.Lng)) return CommandResult<CrimeViewModel>.Failure(ErrorMessages.Generic.NotWithinUK);

                var response = await apiCaller.SendGetRequestAsync(BuildGetCrimesRequest(request));
                if (response == null) return CommandResult<CrimeViewModel>.Failure(ErrorMessages.Generic.SomethingWentWrong);

                var model = JsonConvert.DeserializeObject<List<CrimeData>>(response);
                if (model == null) return CommandResult<CrimeViewModel>.Failure(ErrorMessages.Generic.SomethingWentWrong);

                var responseModel = new CrimeViewModel()
                {
                    CrimeCount = model.Count,
                    CrimeData = [.. model.OrderBy(x => x.Category)]
                };

                return CommandResult<CrimeViewModel>.Success(responseModel);

            }
            catch (Exception ex)
            {
                return CommandResult<CrimeViewModel>.Failure(ErrorMessages.Generic.SomethingWentWrong);
            }

        }

        private string BuildGetCrimesRequest(GetCrimesRequest request) => $"{_configuration.GetSection("Urls:PoliceApiUrl").Value}crimes-street/all-crime?lat={request.Lat}&lng={request.Lng}&date={_dateHelper.GetLastYearMonth(request.Month)}";
    }
}
