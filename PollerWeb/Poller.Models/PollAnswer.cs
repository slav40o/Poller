using System.ComponentModel.DataAnnotations;

namespace Poller.Models
{
    public class PollAnswer : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        public int PollQuestionId { get; set; }

        [Required]
        public PollQuestion PollQuestion { get; set; }
    }
}