using System;
using FluentValidation;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AskAQuestion.Api.Requests;

public class CommentRequests
{
    public static async Task<IResult> GetAllByQuestionId(ICommentRepository commentRepository
        , IQuestionRepository questionRepository
        , int questionId)
    {
        var questionExist = await questionRepository.QuestionExists(questionId);

        if (questionExist)
        {
            var comments = await commentRepository.GetAllByQuestionId(questionId);
            return Results.Ok(comments);
        }

        return Results.NotFound();
    }

    public static async Task<IResult> GetAllByUserId(ICommentRepository commentRepository,
        IUserRepository userRepository, Guid userId)
    {
        var userExists = await userRepository.UserExists(userId);

        if (userExists)
        {
            var comments = await commentRepository.GetAllByUserId(userId);
            return Results.Ok(comments);
        }

        return Results.NotFound();
    }

    public static async Task<IResult> GetById(ICommentRepository commentRepository, int commentId)
    {
        var comment = await commentRepository.GetById(commentId);

        if (comment is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(comment);
    }

    [Authorize]
    public static async Task<IResult> Create(ICommentRepository commentRepository,
        IQuestionRepository questionRepository, IValidator<CreateCommentDto> validator, CreateCommentDto commentDto)
    {
        var validateResult = validator.Validate(commentDto);
        if (!validateResult.IsValid)
        {
            return Results.BadRequest(validateResult.Errors);
        }

        var questionExists = await questionRepository.QuestionExists(commentDto.QuestionId);
        if (questionExists)
        {
            Comment comment = new()
            {
                QuestionId = commentDto.QuestionId,
                UserId = commentDto.UserId,
                Content = commentDto.Content,
                CreateDate = DateTime.Now
            };
            await commentRepository.Create(comment);
            return Results.Created($"question/{comment.QuestionId}/comment/{comment.Id}", comment);
        }

        return Results.NotFound();

    }

    [Authorize]
    public static async Task<IResult> Update(ICommentRepository commentRepository, UpdateCommentDto commentDto, IValidator<UpdateCommentDto> validator)
    {
        var validateResult = validator.Validate(commentDto);
        if (!validateResult.IsValid)
        {
            return Results.BadRequest(validateResult.Errors);
        }
        var commentExists = await commentRepository.CommentExists(commentDto.Id);
        if (commentExists)
        {         
            var comment = await commentRepository.Update(commentDto);
            return Results.Ok(comment);
        }
        return Results.NotFound();
    }

    [Authorize]
    public static async Task<IResult> Delete(ICommentRepository commentRepository, int commentId)
    {
        var comment = await commentRepository.GetById(commentId);
        if (comment is null)
        {
            return Results.NotFound();
        }
        await commentRepository.Delete(comment);
        return Results.Ok();
    }
}

