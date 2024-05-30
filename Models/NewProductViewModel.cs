using AgriEnergyConnectPOE.Data;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectPOE.Models
{
    public class NewProductViewModel
    {
       
        
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]  
        public string SelectedCategoryName { get; set; }
        
        [Required] 
        public List<string> CategoryNames { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public string ImagePath { get; set; }

      
    }
}
