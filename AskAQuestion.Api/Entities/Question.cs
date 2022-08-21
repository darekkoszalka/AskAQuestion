namespace AskAQuestion.Api.Entities;

public class Question : EntryBase
{
    public string Title { get; set; }
    public virtual List<Comment> Comments { get; set; }

    public virtual List<QuestionTag> Tags { get; set; }
}