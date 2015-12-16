namespace Poller.Web.Controllers
{
    using Poller.Common.Managers;
    using Poller.Data;
    using Poller.Models;
    using System.Web.Mvc;

    public class PollController : Controller
    {
        private PollManager manager;

        public PollController()
        {
            // Just for test at app harbor. Fix later with iOC container
            manager = new PollManager(new GenericRepository<Poll>(new PollerDb()));
        }
        // GET: Poll
        public ActionResult Index()
        {
            var polls = manager.GetTopPolls();
            return View(polls);
        }
    }
}