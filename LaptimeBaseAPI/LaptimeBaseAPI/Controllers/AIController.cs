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
            var payload = new
            {
                question = question.Question,
                additional_data = question.AdditionalData
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/ask", payload);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<object>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
