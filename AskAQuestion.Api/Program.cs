using AskAQuestion.Api.Configurations.DependencyInjections;
using AskAQuestion.Api.Data;
using AskAQuestion.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddAskAQuestionDbContext();
builder.AddJsonOptions();
builder.AddJwtBearerAuthorization();
builder.AddEntitiesValidator();
builder.AddEntityRepositories();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.DataSeeder().Wait();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UserEndPoints();
app.QuestionEndPoints();
app.CommentEndPoints();
app.LikesEndpoints();

app.Run();



