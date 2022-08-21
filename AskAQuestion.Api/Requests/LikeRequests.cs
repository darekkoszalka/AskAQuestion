using System;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AskAQuestion.Api.Requests;

public class LikeRequests
{
    [Authorize]
    public static async Task<IResult> Create(ILikeRepository likeRepository, Like like)
    {
        var likeExists = await likeRepository.GetLikeById(like.Id);
        if(likeExists is not null)
        {
            return Results.Problem("Like is already added.");
        }
        await likeRepository.Create(like);
        return Results.Ok();
    }

    [Authorize]
    public static async Task<IResult> Delete(ILikeRepository likeRepository, int likeId)
    {
        var like = await likeRepository.GetLikeById(likeId);
        if(like is null)
        {
            return Results.NotFound();
        }
        return Results.Ok();
    }

    public static async Task<IResult> GetAllEntryLikes(ILikeRepository likeRepository,
        IEntryRepository entryRepository, int entryId)
    {
        var entryExists = await entryRepository.EntryExists(entryId);
        if(entryExists)
        {
            var likes = await likeRepository.GetAllEntryLikes(entryId);
            return Results.Ok(likes);
        }

        return Results.NotFound();
    }
}

