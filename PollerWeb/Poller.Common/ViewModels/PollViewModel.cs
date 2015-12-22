namespace Poller.Common.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class PollViewModel : PollSimpleViewModel
    {
        public PollViewModel()
        {
        }

        public PollViewModel(Poll poll)
        {
            
        }

        public IEnumerable<PollQuestion> Questions { get; set; }

        public int PageSize { get; set; }
    }
}
