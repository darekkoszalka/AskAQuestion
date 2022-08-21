using AskAQuestion.Api.Dto;
using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> Create(Comment comment);
        Task Delete(Comment comment);
        Task<List<Comment>> GetAllByQuestionId(int questionId);
        Task<List<Comment>> GetAllByUserId(Guid userId);
        Task<Comment> Update(UpdateCommentDto commentDto);
        Task<Comment> GetById(int commentId);
        Task<bool> CommentExists(int commentId);
    }
}