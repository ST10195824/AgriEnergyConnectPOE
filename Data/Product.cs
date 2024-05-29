using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectPOE.Data
{
    public class Product
    {

        public int ProductId { get; set; }  // Primary Key
        public string UserId { get; set; }  // Foreign Key to ApplicationUser

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        [Required]
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }  // Foreign Key to Category

        [Required]
        public DateTime ProductionDate { get; set; }

        public string ImagePath { get; set; }

        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
    }
}
