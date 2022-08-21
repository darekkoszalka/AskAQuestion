using System;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Dto;

public class CreateQuestionDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public List<int> Tags { get; set; } = new();

}

