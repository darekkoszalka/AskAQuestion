using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace AskAQuestion.Api.Configurations.DependencyInjections;

public static class JsonOptionsDependency
{
    public static WebApplicationBuilder AddJsonOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(option =>
        {
            option.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        return builder;
    }
}

