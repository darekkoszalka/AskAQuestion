using System;
namespace AskAQuestion.Api.Dto
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public int QuestionId { get; set; }
    }
}

