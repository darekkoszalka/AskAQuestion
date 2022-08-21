using System;
using AskAQuestion.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AskAQuestion.Api.Data
{
    public class AskAQuestionDbContext : DbContext
    {
        
        public AskAQuestionDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<EntryBase> EntryBase { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}

