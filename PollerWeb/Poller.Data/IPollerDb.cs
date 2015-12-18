using System.Data.Entity;
using Poller.Models;

namespace Poller.Data
{
    public interface IPollerDb
    {
        IDbSet<PollAnswer> PollAnswers { get; set; }
        IDbSet<PollQuestion> PollQuestions { get; set; }
        IDbSet<Poll> Polls { get; set; }
    }
}