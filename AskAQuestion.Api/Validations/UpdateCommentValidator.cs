using System;
using FluentValidation;
using AskAQuestion.Api.Dto;

namespace AskAQuestion.Api.Validations
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Field 'id' is required");

            RuleFor(c => c.Content)
                .NotEmpty()
                .WithMessage("Field 'content' is required.")
                .MaximumLength(500)
                .WithMessage("Field 'content' can't have more than 500 chars");
         


        }
    }
}

