using System;
using AskAQuestion.Api.Configurations.AuthenticationConfiguration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AskAQuestion.Api.Configurations.DependencyInjections
{
    public static class JwtBearerDependency
    {
        public static WebApplicationBuilder AddJwtBearerAuthorization(this WebApplicationBuilder builder)
        {
            AuthenticationSetting authenticationSettings = new();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            builder.Services.AddSingleton(authenticationSettings);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))

                };
            });
            builder.Services.AddAuthorization();

            return builder;
        }
    }
}

