using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectPOE.Controllers
{
    public class MarketPlaceController: Controller
    {
        private readonly IMarketPlace _marketPlace;
        private readonly ImageService _imageService;
        private readonly ApplicationDbContext _applicationDbContext;

        public MarketPlaceController(ApplicationDbContext applicationDbContext, IMarketPlace marketPlace, ImageService imageService)
        {
            _applicationDbContext = applicationDbContext;
            _marketPlace = marketPlace;
            //might not need this 
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var ViewModel = new MarketPlaceViewModel();
            ViewModel.DisplayedProducts = _applicationDbContext.Products.ToList();
            return View(ViewModel);
        }
    }
}
