using Shared.Session;

namespace Shared.AIQuestion;

public class QuestionForAI
{
    public string Question { get; set; }

    public SessionDto AdditionalData { get; set; }
}
