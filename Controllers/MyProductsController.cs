using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectPOE.Controllers
{
    public class MyProductsController: Controller
    {
        private readonly IMarketPlace _marketPlace;
        private readonly ImageService _imageService;
        private readonly ApplicationDbContext _applicationDbContext;

        public MyProductsController(ApplicationDbContext applicationDbContext, IMarketPlace marketPlace, ImageService imageService)
        {
            _applicationDbContext = applicationDbContext;
            _marketPlace = marketPlace;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var ViewModel = new MarketPlaceViewModel();
            ViewModel.DisplayedProducts = _marketPlace.GetCurrentUsersProducts(); 
            return View(ViewModel);
        }

        public IActionResult AddProduct()
        {
            var ViewModel = new NewProductViewModel();
            ViewModel.CategoryNames = _applicationDbContext.Categories.Select(c => c.CategoryName).ToList();
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(NewProductViewModel viewModel, IFormFile ImageFile)
        {
            
            
                var categoryID = _marketPlace.getCategoryIdByName(viewModel.SelectedCategoryName);

                string imagePath = null;
                if (ImageFile != null)
                {
                    imagePath = await _imageService.SaveImageAsync(ImageFile);
                }

                

                _marketPlace.AddProduct(
                    viewModel.ProductName,
                    viewModel.ProductPrice,
                    viewModel.ProductDescription,
                    categoryID,
                    viewModel.ProductionDate,
                    imagePath ?? ""
                );
                return RedirectToAction("Index");
            
            
        }
    }




}

