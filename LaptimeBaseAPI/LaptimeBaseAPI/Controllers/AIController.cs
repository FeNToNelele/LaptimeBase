using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.AIQuestion;

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
        public async Task<IActionResult> AskAI([FromBody] QuestionForAI question)
        {
            var response = await _httpClient.PostAsJsonAsync("/ask", new { Question = question });
            return Ok(await response.Content.ReadFromJsonAsync<object>());
        }
    }
}
