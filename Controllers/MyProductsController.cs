using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectPOE.Controllers
{
    public class MyProductsController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
