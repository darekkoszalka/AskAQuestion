using System;
using AskAQuestion.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AskAQuestion.Api.Configurations.EntitiesConfigurations
{
    public class EntryBaseConfiguration : IEntityTypeConfiguration<EntryBase>
    {

        public void Configure(EntityTypeBuilder<EntryBase> builder)
        {
            builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(1000);
            builder.Property(e => e.CreateDate)
                .IsRequired();

        }
    }
}

