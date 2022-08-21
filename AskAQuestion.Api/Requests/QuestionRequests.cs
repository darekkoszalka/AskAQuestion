using System;
using FluentValidation;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Repositories;

namespace AskAQuestion.Api.Requests;

public class QuestionRequests
{
    public static async Task<IResult> GetAll(IQuestionRepository questionRepository)
    {
        var questions = await questionRepository.GetAll();
        return Results.Ok(questions);
    }

    public static async Task<IResult> GetAllByUserId(IQuestionRepository questionRepository, Guid userId)
    {
        var questions = await questionRepository.GetAllByUserId(userId);
        return Results.Ok(questions);
    }

    public static async Task<IResult> GetById(IQuestionRepository questionRepository, int questionId)
    {
        var question = await questionRepository.GetById(questionId);
        if(question is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(question);
    }

    public static async Task<IResult> Create(IQuestionRepository questionRepository, CreateQuestionDto questionDto, IValidator<CreateQuestionDto> validatior)
    {
        var validateResult = validatior.Validate(questionDto);
        if(!validateResult.IsValid)
        {
            return Results.BadRequest(validateResult.Errors);
        }

        Question question = new()
        {
            Title = questionDto.Title,
            Content = questionDto.Content,
            CreateDate = DateTime.Now,
            UserId = questionDto.UserId,
        };
       var tags = questionDto.Tags.ToHashSet().ToList();

        await questionRepository.Create(question, tags);
        return Results.Created($"question/{question.UserId}",question);
    }

    public static async Task<IResult> Delete(IQuestionRepository questionRepository, int questionId)
    {
        var question = await questionRepository.GetById(questionId);
        if (question is null)
        {
            return Results.NotFound();
        }

        await questionRepository.Delete(question);
        return Results.Ok();
    }

    public static async Task<IResult> Update(IQuestionRepository questionRepository, UpdateQuestionDto questionDto, IValidator<UpdateQuestionDto> validator)
    {
        var validateResult = validator.Validate(questionDto);
        if(!validateResult.IsValid)
        {
            return Results.BadRequest(validateResult.Errors);
        }
        var questionExists = await questionRepository.QuestionExists(questionDto.Id);
        if (questionExists is false)
        {
            return Results.NotFound();
        }
        await questionRepository.Update(questionDto);
        return Results.Ok();
    }
}

