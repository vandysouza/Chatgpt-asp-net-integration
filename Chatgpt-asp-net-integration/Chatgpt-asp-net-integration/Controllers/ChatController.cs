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
                Temperature = 0.7, // Ajuste o valor conforme necessário
                TopP = 0.9, // Ajuste o valor conforme necessário
                FrequencyPenalty = 0.5, // Ajuste o valor conforme necessário
                PresencePenalty = 0.3 // Ajuste o valor conforme necessário
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
                // Lidar com exceções, se necessário
                return StatusCode(500, "Erro ao processar a solicitação: " + ex.Message);
            }

            return Ok(response);
        }
    }
}
