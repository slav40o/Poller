namespace Poller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Poll : IEntity
    {
        private ICollection<PollQuestion> questions;

        public Poll()
        {
            questions = new HashSet<PollQuestion>();
        }

        public Guid Id { get; set; }

        public string CreatorId { get; set; }

        [Required]
        public virtual ApplicationUser Creator { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int PageSize { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [Index]
        public int ParticipientsCount { get; set; }

        public virtual ICollection<PollQuestion> Questions
        {
            get { return questions; }
            set { questions = value; }
        }
    }
}
