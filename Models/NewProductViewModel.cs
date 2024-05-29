using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Models
{
    public class NewProductViewModel
    {
       
        

        public string ProductName { get; set; }

 
        public decimal ProductPrice { get; set; }

  
        public string ProductDescription { get; set; }

        public string SelectedCategoryName { get; set; }
      
        public List<string> CategoryNames { get; set; }

        public DateTime ProductionDate { get; set; }

        public string ImagePath { get; set; }

      
    }
}
