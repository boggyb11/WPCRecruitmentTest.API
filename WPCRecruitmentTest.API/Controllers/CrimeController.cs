using Microsoft.AspNetCore.Mvc;
using WPCRecruitmentTest.Services.Interfaces;
using WPCRecruitmentTest.ViewModels.Requests;

namespace WPCRecruitmentTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrimeController(ICrimeService crimeService) : ControllerBase
    {

        private readonly ICrimeService _crimeService = crimeService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCrimes(GetCrimesRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            var result = await _crimeService.GetCrimes(request);

            if (result.Succeeded)
                return Ok(result.Content);

            return BadRequest(result.Error);
        }
    }
}
