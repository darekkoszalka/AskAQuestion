using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Validations;

public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
{
    private readonly AskAQuestionDbContext _context;

    public CreateQuestionDtoValidator(AskAQuestionDbContext context)
    {
        _context = context;

        RuleFor(e => e.Content)
            .NotEmpty()
            .WithMessage("Field 'Content' is Required")
            .MaximumLength(1000)
            .WithMessage("Field 'Content' can't have more than 1000 chars");
        RuleFor(e => e.Title)
            .NotEmpty()
            .WithMessage("Field 'Title' is Required")
            .MaximumLength(1000)
            .WithMessage("Field 'Title' can't have more than 50 chars");
        RuleFor(e => e.UserId)
            .NotEmpty()
            .WithMessage("Field 'UserId' is Required");
        RuleFor(e => e.Tags.Count)
            .LessThanOrEqualTo(5)
            .WithMessage("It can add only 5 tags");
        

    }
}

