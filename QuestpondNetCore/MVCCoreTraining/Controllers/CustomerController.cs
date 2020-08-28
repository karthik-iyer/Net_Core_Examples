using Microsoft.AspNetCore.Mvc;

namespace MVCCoreTraining.Controllers
{
    public class CustomerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View("CustomerAdd");
        }

        public IActionResult Rec(string val)
        {
            ViewBag.val = val;
            return View("Rec");
        }
    }
}