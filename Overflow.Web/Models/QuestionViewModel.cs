namespace Overflow.Web.Models;

public class QuestionViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string AskerId { get; set; } = string.Empty;
    public string AskerDisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ViewCount { get; set; }
    public List<string> TagSlugs { get; set; } = [];
    public bool HasAcceptedAnswer { get; set; }
    public int Votes { get; set; }
    public int AnswerCount { get; set; }
    public List<AnswerViewModel> Answers { get; set; } = [];
}

public class AnswerViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserDisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Accepted { get; set; }
}
