using GroqApiLibrary;
using LaptimeBaseAPI.Helper;
using LaptimeBaseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LaptimeBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroqAPIController : ControllerBase
    {
        private readonly GroqApiClient groqApiClient;
        public GroqAPIController()
        {
            groqApiClient = new GroqApiClient(Environment.GetEnvironmentVariable("LAPTIMEBASE_API_KEY"));
        }

        // GET: api/groq/getInsights
        [HttpGet("getInsights")]
        public async Task<ActionResult<string>> GetInsights()
        {
            // shows insights on one race's results

            //todo: send request to Python API


            return "This is a test.";
        }
    }
}
