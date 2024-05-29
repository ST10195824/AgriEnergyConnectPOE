using AgriEnergyConnectPOE.Data;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE.Services
{
    public class MarketPlaceService: IMarketPlace
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MarketPlaceService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUser? CurrentUser
        {
            get
            {
                return (ApplicationUser?)(_context.Users?.FirstOrDefault(predicate: u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name));
            }
        }

        public void SetCurrentUser(ApplicationUser currentUser)
        {
            // This method can be used to set the current user manually if needed.
            // For now, we are fetching the current user based on the HTTP context.
        }

        public void AddProduct(string pName, decimal pPrice, string pDescription, int pCategoryId, DateTime pProductionDate, string pImagePath = null)
        {
            var product = new Product
            {
                ProductName = pName,
                ProductPrice = pPrice,
                ProductDescription = pDescription,
                CategoryId = pCategoryId,
                ProductionDate = pProductionDate,
                ImagePath = pImagePath,
                UserId = CurrentUser.Id
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void AddCategory(string categoryName, string categoryDescription)
        {
            var category = new Category
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescription
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public int getCategoryIdByName(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            return category?.CategoryId ?? 0;
        }

        public List<Product> GetCurrentUsersProducts()
        {
            List<Product> products = _context.Products.Where(p => p.UserId == CurrentUser.Id).ToList();

            return products;
        }
    }
}
