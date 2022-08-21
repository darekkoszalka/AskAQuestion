using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AskAQuestion.Api.Configurations.AuthenticationConfiguration;
using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;
using AskAQuestion.Api.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AskAQuestion.Api.Requests;

public static class UserRequests
{
    [Authorize(Roles = "Admin")]
    public static async Task<IResult> GetAllUsers(IUserRepository userRepository)
    {
        var users = await userRepository.GetAllUsers();
        return Results.Ok(users);
    }

    [AllowAnonymous]
    public static async Task<IResult> RegisterUser(IUserRepository userRepository, IPasswordHasher<User> _passwordHasher, IValidator<RegisterUserDto> validator, RegisterUserDto registerUserDto)
    {
        var validateResult = validator.Validate(registerUserDto);
        if (!validateResult.IsValid)
        {
            return Results.BadRequest(validateResult.Errors);
        }
        User newUser = new()
        {
            Email = registerUserDto.Email,
            RegisterDate = DateTime.Now,
            Name = registerUserDto.Name,
        };

        var hashPasswor = _passwordHasher.HashPassword(newUser, registerUserDto.Password);

        newUser.PasswordHash = hashPasswor;
        await userRepository.RegisterUser(newUser);

        UserRole userRole = new()
        {
            UserId = newUser.Id,
            RoleId = 1
        };

        await userRepository.CreateUserRole(userRole);

        return Results.Created($"user/{newUser.Id}", newUser);

    }

    [Authorize(Roles = "Admin")]
    public static async Task<IResult> GetUserById(IUserRepository userRepository, Guid id)
    {
        var user = await userRepository.GetUserById(id);
        if (user is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(user);

    }

    [Authorize(Roles = "Admin")]
    public static async Task<IResult> DeleteUser(IUserRepository userRepository, Guid id)
    {
        var user = await userRepository.GetUserById(id);
        if (user is null)
        {
            return Results.NotFound();
        }

        await userRepository.DeleteUser(user);
        return Results.Ok();
    }

    [Authorize(Roles = "Admin")]
    public static async Task<IResult> UpdateUser(IUserRepository userRepository, User user)
    {
        var userExists = await userRepository.UserExists(user.Id);
        if (userExists is false)
        {
            return Results.NotFound();
        }

        await userRepository.UpdateUser(user);
        return Results.Ok();
    }

    [AllowAnonymous]
    public static async Task<IResult> GetToken(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, AuthenticationSetting authenticationSetting, LoginDto loginDto)
    {
        var userExists = await userRepository.GetUserByEmail(loginDto.Email);
        if(userExists is null)
        {
            return Results.BadRequest("Invalid user name or password.");
        }


        var result = passwordHasher.VerifyHashedPassword(userExists, userExists.PasswordHash, loginDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return Results.BadRequest("Invalid user name or password.");
        }

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, userExists.Id.ToString()),
            new Claim(ClaimTypes.Name, userExists.Name),
            new Claim(ClaimTypes.Role, userExists.UserRoles[0].Role.Name)
            
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(authenticationSetting.JwtExpireDays);

        var token = new JwtSecurityToken(authenticationSetting.JwtIssuer, authenticationSetting.JwtIssuer, claims, expires: expires, signingCredentials: credential);

        var tokenHandler = new JwtSecurityTokenHandler();

        return Results.Ok(tokenHandler.WriteToken(token));
    }


}

