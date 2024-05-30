using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AgriEnergyConnectPOE.Controllers
{
    //------------------------------------------------------------------------------------//
    //                          *Farmers Personal Page Controller*                        //
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Controller for managing the farmer's products.
    /// </summary>
    [Authorize(Roles = "Farmer")]
    public class MyProductsController: Controller
    {

        //----------------------------------------------------------//
        //                          *Fields*                        //
        //----------------------------------------------------------//
        private readonly ImageService _imageService;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUser? _currentUser;

        //-------------------------------*Constructor*-------------------------------//
        /// <summary>
        /// Initializes a new instance of the <see cref="MyProductsController"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="userStore">The user store.</param>
        /// <param name="imageService">The image service.</param>
        public MyProductsController(ApplicationDbContext applicationDbContext, ICurrentUserSingleton userStore, ImageService imageService)
        {
            _context = applicationDbContext;
            _imageService = imageService;
            _currentUser = userStore.CurrentUser;
        }

        //-------------------------------*Farmers Product Page*-------------------------------//
        /// <summary>
        /// Displays the farmer's products.
        /// </summary>
        /// <returns>The view containing the farmer's products.</returns>
        public IActionResult Index()
        {
            if (_currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var ViewModel = new MarketPlaceViewModel();
            ViewModel.DisplayedProducts = _context.Products.Where(p => p.UserId == _currentUser.Id).ToList();
            return View(ViewModel);
        }

        //-------------------------------*Display Add Product View*-------------------------------//
        /// <summary>
        /// Displays the form for adding a new product.
        /// </summary>
        /// <returns>The view containing the form for adding a new product.</returns>
        public IActionResult AddProduct()
        {
            var ViewModel = new NewProductViewModel();
            ViewModel.CategoryNames = _context.Categories.Select(c => c.CategoryName).ToList();
            return View(ViewModel);
        }

        //-------------------------------*Add New Product Action*-------------------------------// 
        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="viewModel">The view model containing the product details.</param>
        /// <param name="ImageFile">The image file of the product.</param>
        /// <returns>The action result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(NewProductViewModel viewModel, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new NewProductViewModel();
                ViewModel.CategoryNames = _context.Categories.Select(c => c.CategoryName).ToList();
                return View(ViewModel);
            }

            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == viewModel.SelectedCategoryName);
            var categoryID = category?.CategoryId ?? 0;

            string imagePath = null;
            if (ImageFile != null)
            {
                imagePath = await _imageService.SaveImageAsync(ImageFile);
            }

            var product = new Product
            {
                ProductName = viewModel.ProductName,
                ProductPrice = viewModel.ProductPrice,
                ProductDescription = viewModel.ProductDescription,
                CategoryId = categoryID,
                ProductionDate = viewModel.ProductionDate,
                ImagePath = imagePath ?? "",
                UserId = _currentUser.Id
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult MyIndividualProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id && p.UserId == _currentUser.Id);

           
            if (product == null)
            {
                return NotFound();
            }

            var productCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            product.Category = productCategory;

            return View("MyIndividualProduct", product);
        }
    }
}
//----------------------------------------------------End_of_File----------------------------------------------------//
