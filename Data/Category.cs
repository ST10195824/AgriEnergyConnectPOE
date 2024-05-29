using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectPOE.Data
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDescription { get; set; }
    }
}
