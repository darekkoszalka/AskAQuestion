using System;
using Microsoft.EntityFrameworkCore;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly AskAQuestionDbContext _context;

    public LikeRepository(AskAQuestionDbContext context)
    {
        _context = context;
    }

    public async Task Create(Like like)
    {
        await _context.Likes.AddAsync(like);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Like like)
    {
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Like>> GetAllEntryLikes(int entryId)
    {
        var likes = await _context.Likes
            .Where(l => l.EntryId == entryId)
            .Include(l => l.Entry)
            .Include(l => l.User)
            .ToListAsync();

        return likes;
    }

    public async Task<Like> GetLikeById(int likeId)
    {
         var like = await _context.Likes.FirstOrDefaultAsync(l => l.Id == likeId);

        return like;
    }
}

