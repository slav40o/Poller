namespace Poller.Common.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using ViewModels;
    using Contracts;
    using Data;
    using Models;

    public class PollManager : IPollManager
    {
        private IRepository<Poll> polls;

        public PollManager(IRepository<Poll> pollsRepo)
        {
            polls = pollsRepo;
        }

        public Poll CreatePoll(ApplicationUser creator, string title, string description)
        {
            return CreatePoll(creator, title, description, true, true, 10);
        }

        public Poll CreatePoll(ApplicationUser creator, string title, string description, bool isActive = true, bool isPublic = true, int pageSize = 10)
        {
            var poll = new Poll
            {
                Creator = creator,
                DateCreated = DateTime.Now,
                Title = title,
                Description = description,
                IsActive = isActive,
                IsPublic = isPublic,
                PageSize = pageSize,
                ParticipientsCount = 0
            };

            polls.Add(poll);
            polls.SaveChanges();
            return poll;
        }

        public IEnumerable<PollSimpleViewModel> GetTopPolls(int count = 10)
        {
            var result = GetTopPollsQuery(count);
            return result.ToList();
        }

        public async Task<IEnumerable<PollSimpleViewModel>> GetTopPollsAsync(int count = 10)
        {
            var result = GetTopPollsQuery(count);
            return await result.ToListAsync();
        }

        public IEnumerable<PollSimpleViewModel> SearchByCreator(string creatorId, int page, int pageSize)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                CreatorId = creatorId
            }, page, pageSize);

            return result.ToList();
        }

        public async Task<IEnumerable<PollSimpleViewModel>> SearchByCreatorAsync(string creatorId, int page, int pageSize)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                CreatorId = creatorId
            }, page, pageSize);

            return await result.ToListAsync();
        }

        public IEnumerable<PollSimpleViewModel> SearchByTitle(string title, int page, int pageSize, bool matchFull = false)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull
            }, page, pageSize);
            return result.ToList();
        }

        public async Task<IEnumerable<PollSimpleViewModel>> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull
            }, page, pageSize);
            return await result.ToListAsync();
        }

        public IEnumerable<PollSimpleViewModel> ComplexSearch(PollSearchParameteres search, int page, int pageSize)
        {
            var result = GetSearchQuery(search, page, pageSize);
            return result.ToList();
        }

        public async Task<IEnumerable<PollSimpleViewModel>> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize)
        {
            var result = GetSearchQuery(search, page, pageSize);
            return await result.ToListAsync();
        }

        public PollViewModel GetFullPollInfo(Guid id)
        {
            var poll = polls.GetById(id);
            var pollModel = new PollViewModel(poll);
            pollModel.Questions = poll.Questions.ToList();
            return pollModel;
        }

        

        private IQueryable<PollSimpleViewModel> GetTopPollsQuery(int count = 10)
        {
            return polls.All()
                .Select(PollSimpleViewModel.FromPoll)
                .Where(p => p.IsActive == true && p.IsPublic == true)
                .OrderBy(p => p.ParticipientsCount)
                .Take(count);
        }

        private IQueryable<PollSimpleViewModel> GetSearchQuery(PollSearchParameteres search, int page, int pageSize)
        {
            var result = polls.All().Select(PollSimpleViewModel.FromPoll);
            if (search.CreatorId != null)
                result = result.Where(p => p.CreatorId == search.CreatorId);

            if (search.FromDate != null)
                result = result.Where(p => p.DateCreated >= search.FromDate);

            if (search.ToDate != null)
                result = result.Where(p => p.DateCreated <= search.ToDate);

            if (search.Тitle != null)
            {
                if (search.MatchFullTitle != null && search.MatchFullTitle == true)
                    result = result.Where(p => p.Title == search.Тitle);
                else
                    result = result.Where(p => p.Title.Contains(search.Тitle));
            }

            if (search.IsActive != null)
                result = result.Where(p => p.IsActive == search.IsActive);

            if (search.IsPublic != null)
                result = result.Where(p => p.IsPublic == search.IsPublic);

            if (search.FromParticipiantsCount != null)
                result = result.Where(p => p.ParticipientsCount >= search.FromParticipiantsCount);

            if (search.ToParticipiantsCount != null)
                result = result.Where(p => p.ParticipientsCount <= search.ToParticipiantsCount);

            return result.Skip(page * pageSize).Take(pageSize);
        }
    }
}
