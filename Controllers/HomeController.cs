using Microsoft.AspNetCore.Mvc;

// handles requests to home page
namespace MySHop.Controllers
{
    public class HomeController : Controller // inherits from Controller
    {
        // GET: /<controllers>/
        public IActionResult Index() // action method - mapped to handle GET-reqs directed at root url
        {
            // server rendered view (returns razor view (Index.cshtml))
            return View();
        }
    }
}