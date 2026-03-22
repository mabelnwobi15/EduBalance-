using Microsoft.AspNetCore.Mvc;

namespace EduBalance.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
