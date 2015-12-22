namespace Poller.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using ViewModels;

    public interface IPollManager
    {
        PollListViewModel ComplexSearch(PollSearchParameteres search, int page, int pageSize);
        Task<PollListViewModel> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize);
        Poll CreatePoll(ApplicationUser creator, string title, string description);
        Poll CreatePoll(ApplicationUser creator, string title, string description, bool isActive = true, bool isPublic = true, int pageSize = 10);
        IEnumerable<PollSimpleViewModel> GetTopPolls(int count = 10);
        Task<IEnumerable<PollSimpleViewModel>> GetTopPollsAsync(int count = 10);
        PollListViewModel SearchByCreator(string creatorId, int page, int pageSize);
        Task<PollListViewModel> SearchByCreatorAsync(string creatorId, int page, int pageSize);
        PollListViewModel SearchByTitle(string title, int page, int pageSize, bool matchFull = false);
        Task<PollListViewModel> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false);

    }
}