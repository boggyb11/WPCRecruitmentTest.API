using System.Net.Http.Headers;
using System.Text;
using WPCRecruitmentTest.Services.Interfaces;

namespace WPCRecruitmentTest.Services.Services
{
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient _httpClient;

        public ApiCaller()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json+v6"));
        }

        public async Task<string> SendPostRequestAsync(string url, string requestBody)
        {
            try
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<string> SendGetRequestAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
