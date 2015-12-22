namespace Poller.Common.ViewModels
{
    using System.Collections.Generic;

    public class PollListViewModel
    {
        public int ResultCount { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int CountPerPage { get; set; }

        public IEnumerable<PollSimpleViewModel> Polls { get; set; }
    }
}
