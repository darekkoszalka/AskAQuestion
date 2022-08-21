using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> Create(Question question, List<int> tags);
        Task Delete(Question question);
        Task<List<Question>> GetAll();
        Task<List<Question>> GetAllByUserId(Guid userId);
        Task<Question> GetById(int questionId);
        Task<bool> QuestionExists(int questionId);
        Task<Question> Update(UpdateQuestionDto questionDto);
    }
}