using Microsoft.AspNetCore.Mvc;

namespace Amoeba.Areas.AdminPanel.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("AdminPanel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
