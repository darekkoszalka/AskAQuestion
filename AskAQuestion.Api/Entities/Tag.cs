namespace AskAQuestion.Api.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<QuestionTag> QuestionTags { get; set; }
}