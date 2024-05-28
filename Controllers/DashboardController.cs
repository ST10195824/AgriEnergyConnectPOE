using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectPOE.Controllers
{
    public class DashboardController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
