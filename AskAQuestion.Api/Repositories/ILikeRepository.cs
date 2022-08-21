using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories;

public interface ILikeRepository
{
    Task Create(Like like);
    Task Delete(Like like);
    Task<List<Like>> GetAllEntryLikes(int entryId);
    Task<Like> GetLikeById(int likeId);
}