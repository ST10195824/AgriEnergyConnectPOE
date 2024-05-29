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

    }
}
