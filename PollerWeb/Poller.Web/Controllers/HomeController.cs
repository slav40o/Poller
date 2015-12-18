namespace Poller.Web.Controllers
{
    using Common.Contracts;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private IPollManager _pollManager;

        public HomeController(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About";

            return View();
        }
    }
}