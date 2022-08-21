using System;
using System.Xml.Linq;

namespace AskAQuestion.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public virtual List<UserRole> UserRoles { get; set; }
    public virtual List<Question> Questions { get; set; }
    public virtual List<Comment> Comments { get; set; }
    public virtual List<Like> Likes { get; set; }
}

