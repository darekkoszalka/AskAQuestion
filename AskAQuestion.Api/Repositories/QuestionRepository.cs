using System;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AskAQuestion.Api.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AskAQuestionDbContext _context;

        public QuestionRepository(AskAQuestionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAll()
        {
            var questions = await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Comments)
                .Include(q => q.Tags)
                .AsNoTracking()
                .ToListAsync();
            return questions;
        }

        public async Task<List<Question>> GetAllByUserId(Guid userId)
        {
            var questions = await _context.Questions
                .Where(q => q.UserId == userId)
                .Include(q => q.Comments)
                .Include(q => q.Tags)
                .AsNoTracking()
                .ToListAsync();

            return questions;
        }

        public async Task<Question> GetById(int questionId)
        {
            var question = await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Comments)
                .Include(q => q.Tags)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == questionId);

            return question;
        }

        public async Task<bool> QuestionExists(int questionId)
        {
            var question = await _context.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == questionId);
            if (question is null) return false;
            return true;
        }

        public async Task<Question> Create(Question question, List<int> tags)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            if (tags.Count > 0)
            {
                foreach (var tagId in tags)
                {
                    QuestionTag questionTag = new()
                    {
                        QuestionId = question.Id,
                        TagId = tagId
                    };
                    await _context.QuestionTags.AddAsync(questionTag);
                }

                await _context.SaveChangesAsync();
            }

            return question;
        }

        public async Task Delete(Question question)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> Update(UpdateQuestionDto questionDto)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == questionDto.Id);

            question.Title = questionDto.Title;
            question.Content = questionDto.Content;
            question.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return question;
        }

    }
}

