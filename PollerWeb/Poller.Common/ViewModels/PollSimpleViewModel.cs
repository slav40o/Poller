using Poller.Models;
using System;
using System.Linq.Expressions;

namespace Poller.Common.ViewModels
{
    public class PollSimpleViewModel
    {
        public static Expression<Func<Poll, PollSimpleViewModel>> FromPoll
        {
            get
            {
                return p => new PollSimpleViewModel
                {
                    AnswerMode = p.PollAnswerMode,
                    CreatorId = p.CreatorId,
                    CreatorName = p.Creator.UserName,
                    DateCreated = p.DateCreated,
                    Description = p.Description,
                    Id = p.Id,
                    IsActive = p.IsActive,
                    IsPublic = p.IsPublic,
                    ParticipientsCount = p.ParticipientsCount,
                    QuestionsCount = p.QuestionsCount,
                    Title = p.Title
                };
            }
        }

        public Guid Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public int ParticipientsCount { get; set; }

        public int QuestionsCount { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublic { get; set; }

        public PollAnswerMode AnswerMode { get; set; }
    }
}
