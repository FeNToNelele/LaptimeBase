using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaptimeBaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : Controller
    {
        private readonly HttpClient _httpClient;


        public AIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        [HttpPost("askai")]
        public async Task<IActionResult> AskAI() //todo: provide data
        {
            object data = null;

            var response = await _httpClient.PostAsJsonAsync("/ask", data);
            return Ok(await response.Content.ReadFromJsonAsync<object>());
        }
    }
}
