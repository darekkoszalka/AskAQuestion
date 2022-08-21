using System;
using FluentValidation;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Validations;


namespace AskAQuestion.Api.Configurations.DependencyInjections;

public static class ValidatorsDependency
{
    public static WebApplicationBuilder AddEntitiesValidator(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateQuestionDto));
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(UpdateQuestionDto));
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCommentDto));
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserDto));
        return builder;
    }

    
}

