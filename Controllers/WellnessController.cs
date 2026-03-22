using Microsoft.AspNetCore.Mvc;

namespace EduBalance.Controllers
{
    public class WellnessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
