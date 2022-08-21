using System;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AskAQuestion.Api.Validations;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    private readonly AskAQuestionDbContext _context;

    public RegisterUserDtoValidator(AskAQuestionDbContext context)
    {
        _context = context;
        _context = context;
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(6);
        RuleFor(x => x.ConfirmPassword)
            .Equal(e => e.Password);
        RuleFor(x => x.Email)
            .Custom((value, context) =>
            {
                var emailInUse = _context.Users.Any(u => u.Email == value);

                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken.");
                }
            }

            );
        
    }


}

