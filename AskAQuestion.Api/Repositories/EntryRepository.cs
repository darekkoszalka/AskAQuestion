using System;
using Microsoft.EntityFrameworkCore;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AskAQuestionDbContext _context;

        public EntryRepository(AskAQuestionDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EntryExists(int entryId)
        {
            var entry = await _context.EntryBase.FirstOrDefaultAsync(e => e.Id == entryId);
            if (entry is null) return false;
            return true;
        }
    }
}

