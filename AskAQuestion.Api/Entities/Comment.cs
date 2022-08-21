namespace AskAQuestion.Api.Entities;

public class Comment : EntryBase
{
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}