using System;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AskAQuestion.Api.Configurations.DependencyInjections;

public static class RepositoriesDependency
{
    public static WebApplicationBuilder AddEntityRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ILikeRepository, LikeRepository>();
        builder.Services.AddScoped<IEntryRepository, EntryRepository>();

        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return builder;
    }

    
}

