namespace Poller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PollQuestion : IEntity
    {
        private ICollection<PollAnswer> answers;

        public PollQuestion()
        {
            answers = new HashSet<PollAnswer>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public Guid PollId { get; set; }

        [Required]
        public virtual Poll Poll { get; set; }

        public QuestionType QuestionType { get; set; }

        public int AnsweredCount { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        public virtual ICollection<PollAnswer> Answers
        {
            get { return answers; }
            set { answers = value; }
        }
    }
}