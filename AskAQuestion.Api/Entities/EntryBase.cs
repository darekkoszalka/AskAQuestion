using System;
namespace AskAQuestion.Api.Entities;

public class EntryBase
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public virtual List<Like> Likes { get; set; }
}

