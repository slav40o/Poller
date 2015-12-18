namespace Poller.Web.Controllers
{
    using Common.Contracts;
    using System.Web.Mvc;

    public class PollController : Controller
    {
        private IPollManager pollManager;

        public PollController(IPollManager manager)
        {
            pollManager = manager;
        }

        // GET: Poll
        public ActionResult Index()
        {
            var polls = pollManager.GetTopPolls();
            return View(polls);
        }
    }
}