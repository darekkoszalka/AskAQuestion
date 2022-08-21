using System;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Requests;

namespace AskAQuestion.Api.Endpoints;

public static class CommentEndpoinsts
{
    public static WebApplication CommentEndPoints(this WebApplication app)
    {
        app.MapGet("question/{questionId}/comment", CommentRequests.GetAllByQuestionId).Produces<List<Comment>>();

        app.MapGet("user/{userId}/comment", CommentRequests.GetAllByUserId).Produces<List<Comment>>();

        app.MapGet("question/comment/{commentId}", CommentRequests.GetById).Produces<Comment>();

        app.MapPost("question/comment", CommentRequests.Create).Produces<Comment>();

        app.MapPut("question/comment", CommentRequests.Update).Produces<Comment>();

        app.MapDelete("question/comment/{commentId}", CommentRequests.Delete);

        return app;
    }
    
}

