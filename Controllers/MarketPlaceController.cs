using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE.Controllers
{
    [Authorize]
    public class MarketPlaceController: Controller
    {
        private readonly ApplicationUser? _currentUser;
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public MarketPlaceController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext, ICurrentUserSingleton currentUser)
        {
            _context = applicationDbContext;
            _currentUser = currentUser.CurrentUser;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
         {
            var ViewModel = new MarketPlaceViewModel();

            ViewModel.DisplayedProducts = _context.Products.Where(p => p.UserId != _currentUser.Id).ToList();

            return View(ViewModel);
        }

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
