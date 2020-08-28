using Microsoft.AspNetCore.Mvc;

namespace MyFirstCoreWebApp.App.Controllers
{
    public class HomeController : Controller
    {
        private ILog _log;
        public HomeController(ILog log)
        {
            _log = log;
        }
        // GET
        public IActionResult Index()
        {
            _log.info("Executing /home/index");
            return View();
        }

        public IActionResult Index([FromServices] ILog log)
        {
            log.info("Inded method executing");
            return View();
        }
    }
}