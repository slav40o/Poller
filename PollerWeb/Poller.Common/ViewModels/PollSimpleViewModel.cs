namespace Poller.Common.ViewModels
{
    using Poller.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

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

        [Required]
        public string CreatorId { get; set; }

        [DisplayName("Creator")]
        public string CreatorName { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Created on")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Answered")]
        public int ParticipientsCount { get; set; }

        [DisplayName("Questions count")]
        public int QuestionsCount { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public PollAnswerMode AnswerMode { get; set; }
    }
}
