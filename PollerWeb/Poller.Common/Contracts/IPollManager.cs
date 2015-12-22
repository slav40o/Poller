namespace Poller.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using ViewModels;

    public interface IPollManager
    {
        IEnumerable<PollSimpleViewModel> ComplexSearch(PollSearchParameteres search, int page, int pageSize);
        Task<IEnumerable<PollSimpleViewModel>> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize);
        Poll CreatePoll(ApplicationUser creator, string title, string description);
        Poll CreatePoll(ApplicationUser creator, string title, string description, bool isActive = true, bool isPublic = true, int pageSize = 10);
        IEnumerable<PollSimpleViewModel> GetTopPolls(int count = 10);
        Task<IEnumerable<PollSimpleViewModel>> GetTopPollsAsync(int count = 10);
        IEnumerable<PollSimpleViewModel> SearchByCreator(string creatorId, int page, int pageSize);
        Task<IEnumerable<PollSimpleViewModel>> SearchByCreatorAsync(string creatorId, int page, int pageSize);
        IEnumerable<PollSimpleViewModel> SearchByTitle(string title, int page, int pageSize, bool matchFull = false);
        Task<IEnumerable<PollSimpleViewModel>> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false);

    }
}