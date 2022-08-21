using System;
using FluentValidation;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Validations
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .WithMessage("Field 'content' is required.")
                .MaximumLength(500)
                .WithMessage("Field 'content' can't have more than 500 characters.");
            RuleFor(c => c.UserId)
                .NotEmpty()
                .WithMessage("Field 'userId is required.");
            RuleFor(c => c.QuestionId)
                .NotEmpty()
                .WithMessage("Field 'questionId' is required");
        }
    }
}

