using Refit;
using Shared.AIQuestion;

namespace WebUI.Services;

public interface IAIService
{
    [Post("/api/ai/askai")]
    Task<object> AskAIAsync([Body] QuestionForAI question);
}