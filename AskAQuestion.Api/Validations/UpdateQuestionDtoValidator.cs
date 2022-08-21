using System;
using FluentValidation;
using AskAQuestion.Api.Dto;

namespace AskAQuestion.Api.Validations
{
    public class UpdateQuestionDtoValidator : AbstractValidator<UpdateQuestionDto>
    {
        public UpdateQuestionDtoValidator()
        {
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
        }
    }
}

