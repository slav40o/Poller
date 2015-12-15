namespace Poller.Models
{
    using System;
    using System.Collections.Generic;

    public class PollQuestion : IEntity
    {
        private ICollection<PollAnswer> answers;

        public PollQuestion()
        {
            answers = new HashSet<PollAnswer>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public Guid PollId { get; set; }

        public virtual Poll Poll { get; set; }

        public QuestionType QuestionType { get; set; }

        public int AnsweredCount { get; set; }

        public int OrderNumber { get; set; }

        public virtual ICollection<PollAnswer> Answers
        {
            get { return answers; }
            set { answers = value; }
        }
    }
}