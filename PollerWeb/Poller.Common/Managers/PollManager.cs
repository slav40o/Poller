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

        public PollListViewModel SearchByCreator(string creatorId, int page, int pageSize)
        {
            return ComplexSearch(new PollSearchParameteres
            {
                CreatorId = creatorId,
                Order = GetDefaultOrder()
            }, page, pageSize);
        }

        public async Task<PollListViewModel> SearchByCreatorAsync(string creatorId, int page, int pageSize)
        {
            return await ComplexSearchAsync(new PollSearchParameteres
            {
                CreatorId = creatorId,
                Order = GetDefaultOrder()
            }, page, pageSize);
        }

        public PollListViewModel SearchByTitle(string title, int page, int pageSize, bool matchFull = false)
        {
            return ComplexSearch(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull,
                Order = GetDefaultOrder()
            }, page, pageSize);
        }

        public async Task<PollListViewModel> SearchByTitleAsync(string title, int page, int pageSize, bool matchFull = false)
        {
            return await ComplexSearchAsync(new PollSearchParameteres
            {
                Тitle = title,
                MatchFullTitle = matchFull,
                Order = GetDefaultOrder()
            }, page, pageSize);
        }

        public PollListViewModel ComplexSearch(PollSearchParameteres search, int page, int pageSize)
        {
            int count;
            var searchQuery = GetSearchQuery(search, out count);
            var orderedQuery = GetOrderByQuery(searchQuery, search.Order);
            var query = orderedQuery.Skip(page * pageSize).Take(pageSize);
            var result = new PollListViewModel
            {
                CountPerPage = pageSize,
                CurrentPage = page,
                PagesCount = CalculatePagesCount(pageSize, count),
                Polls = query.ToList(),
                ResultCount = count
            };

            return result;
        }

        public async Task<PollListViewModel> ComplexSearchAsync(PollSearchParameteres search, int page, int pageSize)
        {
            int count;
            var searchQuery = GetSearchQuery(search, out count);
            var orderedQuery = GetOrderByQuery(searchQuery, search.Order);
            var query = orderedQuery.Skip(page * pageSize).Take(pageSize);
            var result = new PollListViewModel
            {
                CountPerPage = pageSize,
                CurrentPage = page,
                PagesCount = CalculatePagesCount(pageSize, count),
                Polls = await query.ToListAsync(),
                ResultCount = count
            };

            return result;
        }

        public PollViewModel GetFullPollInfo(Guid id)
        {
            var poll = polls.GetById(id);
            var pollModel = new PollViewModel(poll);
            pollModel.Questions = poll.Questions.ToList();
            return pollModel;
        }

        private int CalculatePagesCount(int pageSize, int count)
        {
            int pageCount = count / pageSize;
            if (count % pageSize > 0)
            {
                pageCount++;
            }

            return pageCount;
        }


        private IEnumerable<Tuple<PollOrderProperty, OrderType>> GetDefaultOrder()
        {
            return new List<Tuple<PollOrderProperty, OrderType>>()
            {
                new Tuple<PollOrderProperty, OrderType>(PollOrderProperty.Title, OrderType.Ascending),
            };
        }

        private IOrderedQueryable<PollSimpleViewModel> GetOrderByQuery(
            IQueryable<PollSimpleViewModel> searchQuery,
            IEnumerable<Tuple<PollOrderProperty, OrderType>> order)
        {
            var query = searchQuery as IOrderedQueryable<PollSimpleViewModel>;
            bool isFirstSet = false;
            foreach (var orderPair in order)
            {
                if (isFirstSet)
                {
                    query = AddThenBy(query, orderPair);
                }
                else
                {
                    isFirstSet = true;
                    query = AddOrderBy(query, orderPair);
                }
            }

            return query;
        }

        private IQueryable<PollSimpleViewModel> GetTopPollsQuery(int count = 10)
        {
            return polls.All()
                .Select(PollSimpleViewModel.FromPoll)
                .Where(p => p.IsActive == true && p.IsPublic == true)
                .OrderBy(p => p.ParticipientsCount)
                .Take(count);
        }

        private IQueryable<PollSimpleViewModel> GetSearchQuery(PollSearchParameteres search, out int count)
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

            count = result.Count();
            return result;
        }
        
        private static IOrderedQueryable<PollSimpleViewModel> AddOrderBy(IOrderedQueryable<PollSimpleViewModel> query, Tuple<PollOrderProperty, OrderType> orderPair)
        {
            switch (orderPair.Item1)
            {
                case PollOrderProperty.CreatorName:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.OrderBy(p => p.CreatorName) :
                        query.OrderByDescending(p => p.CreatorName); break;
                case PollOrderProperty.DateCreated:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.OrderBy(p => p.DateCreated) :
                        query.OrderByDescending(p => p.DateCreated); break;
                case PollOrderProperty.ParticipientsCount:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.OrderBy(p => p.ParticipientsCount) :
                        query.OrderByDescending(p => p.ParticipientsCount); break;
                case PollOrderProperty.QuestionsCount:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.OrderBy(p => p.QuestionsCount) :
                        query.OrderByDescending(p => p.QuestionsCount); break;
                case PollOrderProperty.Title:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.OrderBy(p => p.Title) :
                        query.OrderByDescending(p => p.Title); break;
                default: throw new ArgumentException("Unknown order parameter");
            }

            return query;
        }

        private static IOrderedQueryable<PollSimpleViewModel> AddThenBy(IOrderedQueryable<PollSimpleViewModel> query, Tuple<PollOrderProperty, OrderType> orderPair)
        {
            switch (orderPair.Item1)
            {
                case PollOrderProperty.CreatorName:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.ThenBy(p => p.CreatorName) :
                        query.ThenByDescending(p => p.CreatorName); break;
                case PollOrderProperty.DateCreated:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.ThenBy(p => p.DateCreated) :
                        query.ThenByDescending(p => p.DateCreated); break;
                case PollOrderProperty.ParticipientsCount:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.ThenBy(p => p.ParticipientsCount) :
                        query.ThenByDescending(p => p.ParticipientsCount); break;
                case PollOrderProperty.QuestionsCount:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.ThenBy(p => p.QuestionsCount) :
                        query.ThenByDescending(p => p.QuestionsCount); break;
                case PollOrderProperty.Title:
                    query = orderPair.Item2 == OrderType.Ascending ?
                        query.ThenBy(p => p.Title) :
                        query.ThenByDescending(p => p.Title); break;
                default: throw new ArgumentException("Unknown order parameter");
            }

            return query;
        }
    }
}
