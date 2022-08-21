using System;
using AskAQuestion.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace AskAQuestion.Api.Configurations.DependencyInjections;

public static class DbContextDependency
{
    public static WebApplicationBuilder AddAskAQuestionDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AskAQuestionDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("AskAQuestionDbConnection"));
        }
);
        return builder;
    }
}

