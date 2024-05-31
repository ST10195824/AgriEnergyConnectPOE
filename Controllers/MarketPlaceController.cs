using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE.Controllers
{
    //------------------------------------------------------------------------------------//
    //                          *Marketplace Controller*                                  //
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Controller for managing the marketplace.
    /// </summary>
    [Authorize]
    public class MarketPlaceController: Controller
    {
        //----------------------------------------------------------//
        //                          *Fields*                        //
        //----------------------------------------------------------//
        private readonly ApplicationUser? _currentUser;
        private readonly ApplicationDbContext _context;

        //-------------------------------*Constructor*-------------------------------//
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketPlaceController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="currentUser">The current user singleton.</param>
        public MarketPlaceController(ApplicationDbContext applicationDbContext, ICurrentUserSingleton currentUser)
        {
            _context = applicationDbContext;
            _currentUser = currentUser.CurrentUser;
        }

        //-------------------------------*Marketplace Main Page*-------------------------------//
        /// <summary>
        /// Displays the main marketplace page with products.
        /// </summary>
        /// <returns>The view containing the marketplace products.</returns>
        public async Task<IActionResult> Index()
        {
            var ViewModel = new MarketPlaceViewModel();
            ViewModel.DisplayedProducts = _context.Products.Where(p => p.UserId != _currentUser.Id).ToList();
            return View(ViewModel);
        }

        //-------------------------------*Display Individual Item*-------------------------------//
        /// <summary>
        /// Displays the details of an individual product.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The view containing the details of the product.</returns>
        public IActionResult IndividualItem(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var productCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            product.Category = productCategory;

            return View("IndividualItem", product);
        }
    }
}
//----------------------------------------------------End_of_File----------------------------------------------------//