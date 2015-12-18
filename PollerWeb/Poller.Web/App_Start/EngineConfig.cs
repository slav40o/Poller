namespace Poller.Web
{
    using System.Web.Mvc;

    public class EngineConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection engines)
        {
            engines.Clear();
            engines.Add(new RazorViewEngine());
        }
    }
}