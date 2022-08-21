using System;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Requests;
using Microsoft.AspNetCore.Authorization;

namespace AskAQuestion.Api.Endpoints;

public static class UserEndpoints
{
    public static WebApplication UserEndPoints(this WebApplication app)
    {
        app.MapGet("user", (UserRequests.GetAllUsers)).Produces<User>();

        app.MapPost("user", (UserRequests.RegisterUser));

        app.MapDelete("user", UserRequests.DeleteUser);

        app.MapGet("user/{id}", UserRequests.GetUserById).Produces<User>();

        app.MapPut("user", (UserRequests.UpdateUser));

        app.MapPost("user/login", UserRequests.GetToken);


        return app;
    }
}

