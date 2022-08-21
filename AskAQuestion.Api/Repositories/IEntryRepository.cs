namespace AskAQuestion.Api.Repositories
{
    public interface IEntryRepository
    {
        Task<bool> EntryExists(int entryId);
    }
}