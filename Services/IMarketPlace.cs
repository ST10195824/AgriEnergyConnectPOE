using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Services
{
    public interface IMarketPlace
    {
        ApplicationUser CurrentUser { get; }

        void SetCurrentUser(ApplicationUser currentUser);

        void AddProduct(string pName, decimal pPrice, string pDescription, int pCategoryId, DateTime pProductionDate, string pImagePath = null);

        void AddCategory(string CategoryName, string CategoryDescription);
        int getCategoryIdByName(string categoryName);
        List<Product> GetCurrentUsersProducts();
    }
}
