using Poller.Common.Contracts;
using Poller.Data;
using Poller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Poller.Common.Managers
{
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

        public IEnumerable<Poll> GetTopPolls(int count = 10)
        {
            var result = GetTopPollsQuery(count);
            return result.ToList();
        }

        public async Task<IEnumerable<Poll>> GetTopPollsAsync(int count = 10)
        {
            var result = GetTopPollsQuery(count);
            return await result.ToListAsync();
        }

        public IEnumerable<Poll> SearchByCreator(string creatorId, int page, int pageSize)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                CreatorId = creatorId
            }, page, pageSize);

            return result.ToList();
        }

        public async Task<IEnumerable<Poll>> SearchByCreatorAsync(string creatorId, int page, int pageSize)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                CreatorId = creatorId
            }, page, pageSize);

            return await result.ToListAsync();
        }

        public IEnumerable<Poll> SearchByTitle(string title, int page, int pageSize, bool matchFull = false)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull
            }, page, pageSize);
            return result.ToList();
        }

        public async Task<IEnumerable<Poll>> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false)
        {
            var result = GetSearchQuery(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull
            }, page, pageSize);
            return await result.ToListAsync();
        }

        public IEnumerable<Poll> ComplexSearch(PollSearchParameteres search, int page, int pageSize)
        {
            var result = GetSearchQuery(search, page, pageSize);
            return result.ToList();
        }

        public async Task<IEnumerable<Poll>> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize)
        {
            var result = GetSearchQuery(search, page, pageSize);
            return await result.ToListAsync();
        }

        private IQueryable<Poll> GetTopPollsQuery(int count = 10)
        {
            return polls.All()
                .Where(p => p.IsActive == true && p.IsPublic == true)
                .OrderBy(p => p.ParticipientsCount)
                .Take(count);
        }

        private IQueryable<Poll> GetSearchQuery(PollSearchParameteres search, int page, int pageSize)
        {
            var result = polls.All();
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
