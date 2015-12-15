namespace Poller.Models
{
    using System;
    using System.Collections.Generic;

    public class Poll : IEntity
    {
        private ICollection<PollQuestion> questions;

        public Poll()
        {
            questions = new HashSet<PollQuestion>();
        }

        public Guid Id { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public int PageSize { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<PollQuestion> Questions
        {
            get { return questions; }
            set { questions = value; }
        }
    }
}
