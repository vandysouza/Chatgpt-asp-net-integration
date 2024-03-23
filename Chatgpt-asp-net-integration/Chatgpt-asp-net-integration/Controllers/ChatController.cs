using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace ChatGPT.ASP.NET.Integration.Controllers
{
    [Route("bot/[controller]")]
    public class ChatController : Controller
    {

        private readonly OpenAIAPI _chatGpt;

        public ChatController(OpenAIAPI chatGpt)
        {
            _chatGpt = chatGpt;
        }

        [HttpGet()]
        public async Task<IActionResult> Chat([FromQuery(Name = "prompt")] string prompt)
        {
            var response = "";

            var completion = new CompletionRequest
            {
                Prompt = prompt,
                Model = Model.Davinci,
                MaxTokens = 200,
                Temperature = 0.7, // Ajuste o valor conforme necess�rio
                TopP = 0.9, // Ajuste o valor conforme necess�rio
                FrequencyPenalty = 0.5, // Ajuste o valor conforme necess�rio
                PresencePenalty = 0.3 // Ajuste o valor conforme necess�rio
            };

            try
            {
                var result = await _chatGpt.Completions.CreateCompletionAsync(completion);
                if (result != null && result.Completions != null && result.Completions.Count > 0)
                {
                    response = result.Completions[0].Text;
                }
            }
            catch (Exception ex)
            {
                // Lidar com exce��es, se necess�rio
                return StatusCode(500, "Erro ao processar a solicita��o: " + ex.Message);
            }

            return Ok(response);
        }
    }
}
