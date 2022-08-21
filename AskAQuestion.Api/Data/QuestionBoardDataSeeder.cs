using System;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Data;

public static class QuestionBoardDataSeeder
{

    public static async Task<WebApplicationBuilder> DataSeeder(this WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<AskAQuestionDbContext>();

        await TagSeeder(context);
        await RoleSeeder(context);

        return builder;
    }

    private static async Task TagSeeder(AskAQuestionDbContext context)
    {
        List<Tag> tags = new()
        {
            new Tag { Name = "C#"},
            new Tag { Name = "Java"},
            new Tag { Name = "C++"},
            new Tag { Name = "javascript"},
            new Tag { Name = "Sql"},
            new Tag { Name = "SqlLite"},
            new Tag { Name = "TSql"}
        };

        if (context.Tags.Any())
        {
            return;
        }

        foreach (var item in tags)
        {
            await context.Tags.AddAsync(item);
        }

        await context.SaveChangesAsync();


    }

    private static async Task RoleSeeder(AskAQuestionDbContext context)
    {
        List<Role> roles = new()
        {
            new Role { Name = "User"},
            new Role { Name = "Admin"}
        };

        if (context.Roles.Any())
        {
            return;
        }

        foreach (var item in roles)
        {
            await context.Roles.AddAsync(item);
        }

        await context.SaveChangesAsync();
    }

}

