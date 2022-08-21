namespace AskAQuestion.Api.Entities;

public class Like
{
    public int Id { get; set; }
    public int EntryId { get; set; }
    public virtual EntryBase Entry { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}