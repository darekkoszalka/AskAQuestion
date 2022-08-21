using System;
namespace AskAQuestion.Api.Dto;

public class UpdateQuestionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

