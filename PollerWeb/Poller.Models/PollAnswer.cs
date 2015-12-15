namespace Poller.Models
{
    using System;

    public class PollAnswer : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Count { get; set; }

        public int OrderNumber { get; set; }

        public int PollQuestionId { get; set; }

        public PollQuestion PollQuestion { get; set; }
    }
}