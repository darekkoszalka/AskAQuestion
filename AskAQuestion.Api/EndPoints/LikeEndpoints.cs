using System;
using AskAQuestion.Api.Requests;

namespace AskAQuestion.Api.Endpoints;

public static class LikeEndpoints
{
    public static WebApplication LikesEndpoints(this WebApplication app)
    {
        app.MapPost("like", LikeRequests.Create);
        app.MapDelete("like/{likeId}", LikeRequests.Delete);
        app.MapGet("like/entry/{entryId}", LikeRequests.GetAllEntryLikes);


        return app;
    }
}

