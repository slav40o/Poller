namespace Poller.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Poller.Models;

    public interface IPollManager
    {
        IEnumerable<Poll> ComplexSearch(PollSearchParameteres search, int page, int pageSize);
        Task<IEnumerable<Poll>> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize);
        Poll CreatePoll(ApplicationUser creator, string title, string description);
        Poll CreatePoll(ApplicationUser creator, string title, string description, bool isActive = true, bool isPublic = true, int pageSize = 10);
        IEnumerable<Poll> GetTopPolls(int count = 10);
        Task<IEnumerable<Poll>> GetTopPollsAsync(int count = 10);
        IEnumerable<Poll> SearchByCreator(string creatorId, int page, int pageSize);
        Task<IEnumerable<Poll>> SearchByCreatorAsync(string creatorId, int page, int pageSize);
        IEnumerable<Poll> SearchByTitle(string title, int page, int pageSize, bool matchFull = false);
        Task<IEnumerable<Poll>> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false);

    }
}