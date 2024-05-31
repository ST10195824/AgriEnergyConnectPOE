using AgriEnergyConnectPOE.Data;
using System.Reflection;

namespace AgriEnergyConnectPOE.Models
{
    public class MarketPlaceViewModel
    {

        public MarketPlaceViewModel()
        {

        }

        public List<Product> DisplayedProducts { get; set; } = new List<Product>();

        public List<ApplicationUser> DisplayedFarmers { get; set; } = new List<ApplicationUser>();

        public List<string> FilterByCategorysList { get; set; }
        public string? SelectedFilterOption { get; set; }
    }
}
