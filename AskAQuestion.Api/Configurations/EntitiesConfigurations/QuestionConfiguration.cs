using System;
using AskAQuestion.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AskAQuestion.Api.Configurations.EntitiesConfigurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasOne(e => e.User)
            .WithMany(u => u.Questions)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(150);
    }
}

