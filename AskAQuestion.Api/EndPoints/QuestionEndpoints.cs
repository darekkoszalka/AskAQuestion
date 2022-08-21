using System;
using Microsoft.AspNetCore.Mvc;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Requests;

namespace AskAQuestion.Api.Endpoints;

public static class QuestionEndpoints
{
    public static WebApplication QuestionEndPoints(this WebApplication app)
    {
        app.MapGet("question", (QuestionRequests.GetAll))
            .Produces<List<Question>>();
            

        app.MapGet("question/user/{userId}", (QuestionRequests.GetAllByUserId))
            .Produces<List<Question>>();

        app.MapPost("question", (QuestionRequests.Create))
            .Produces<Question>()
            .RequireAuthorization();

        app.MapDelete("question/{questionId}", QuestionRequests.Delete)
            .RequireAuthorization();

        app.MapGet("question/{questionId}", QuestionRequests.GetById).Produces<Question>();

        app.MapPut("question", (QuestionRequests.Update)).Produces<UpdateQuestionDto>()
            .RequireAuthorization();


        return app;
    }
}

