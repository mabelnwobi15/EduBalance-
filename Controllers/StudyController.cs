using Microsoft.AspNetCore.Mvc;

namespace EduBalance.Controllers
{
    public class StudyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
