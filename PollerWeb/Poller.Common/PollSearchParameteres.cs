namespace Poller.Common
{
    using System;

    public class PollSearchParameteres
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Тitle { get; set; }

        public bool? MatchFullTitle { get; set; }

        public int? FromParticipiantsCount { get; set; }

        public int? ToParticipiantsCount { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsPublic { get; set; }

        public string CreatorId { get; set; }
    }
}
