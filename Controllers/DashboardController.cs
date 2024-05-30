using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class DashboardController: Controller
    {
        private readonly ImageService _imageService;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUser? _currentUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext, ICurrentUserSingleton currentUser, ImageService imageService)
        {
            _context = applicationDbContext;
            _imageService = imageService;
            _currentUser = currentUser.CurrentUser;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            if (_currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var viewModel = new MarketPlaceViewModel();
            var role = await _roleManager.FindByNameAsync("Farmer");
            viewModel.DisplayedFarmers = (List<ApplicationUser>)await _userManager.GetUsersInRoleAsync(role.Name);
            viewModel.DisplayedProducts = _context.Products.Select(p => p).ToList();
            return View(viewModel);
        }

        public IActionResult AddCategory()
        {
            var viewModel = new Category();
            return View(viewModel);
        }

        public IActionResult AddFarmer()
        {
            var viewModel = new FarmerCreationViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == viewModel.CategoryName);
                if (existingCategory != null)
                {
                    ModelState.AddModelError("Name", "Category with the same name already exists.");
                    return View(viewModel);
                }

                await _context.Categories.AddAsync(viewModel);
                await _context.SaveChangesAsync();

                return View(viewModel);
            }

            return View(viewModel);
        }

        public IActionResult FarmerDetails(string id)
        {
            var viewModel = new FarmerCreationViewModel();
            viewModel.FarmersProducts = _context.Products.Where(p => p.UserId == id).ToList();
            ApplicationUser farmer = (ApplicationUser)_context.Users.FirstOrDefault(u => u.Id == id);
            if (farmer != null)
            {
                viewModel.UserName = farmer.UserName;
                viewModel.Email = farmer.Email;
                viewModel.FirstName = farmer.FirstName;
                viewModel.Surname = farmer.Surname;
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(FarmerCreationViewModel viewModel)
        {
           
                var user = new ApplicationUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    Surname = viewModel.Surname
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Farmer");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            

            return View(viewModel);
        }

    }

}


