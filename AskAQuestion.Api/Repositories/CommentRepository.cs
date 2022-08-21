using System;
using Microsoft.EntityFrameworkCore;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AskAQuestionDbContext _context;

    public CommentRepository(AskAQuestionDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllByQuestionId(int questionId)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.QuestionId == questionId)
            .AsNoTracking()
            .ToListAsync();

        return comments;
    }

    public async Task<List<Comment>> GetAllByUserId(Guid userId)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return comments;
    }

    public async Task<Comment> GetById(int commentId)
    {
        var comment = await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Question)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == commentId);

        return comment;
    }

    public async Task<bool> CommentExists(int commentId)
    {
        var comment = await _context.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment is null) return false;
        return true;
    }

    public async Task<Comment> Create(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> Update(UpdateCommentDto commentDto)
    {
       var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentDto.Id);
        comment.Content = commentDto.Content;
        comment.UpdateDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return comment;
        
    }

    public async Task Delete(Comment comment)
    {
        _context.Comments.Remove(comment);      
        await _context.SaveChangesAsync();
    }
}

