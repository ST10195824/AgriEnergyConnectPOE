using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectPOE.Controllers
{
    public class MarketPlaceController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
