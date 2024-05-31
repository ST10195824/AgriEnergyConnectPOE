using AgriEnergyConnectPOE.Data;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectPOE.Models
{
    public class NewProductViewModel
    {
       
        
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

     
        public string SelectedCategoryName { get; set; }
        
 
        public List<string> CategoryNames { get; set; }

        [Required]
        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        public string ImagePath { get; set; }

      
    }
}
