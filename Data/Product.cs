namespace AgriEnergyConnectPOE.Data
{
    public class Product
    {
        public int ProductId { get; set; }  // Primary Key
        public string UserId { get; set; }  // Foreign Key to ApplicationUser
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }  // Foreign Key to Category
        public DateTime ProductionDate { get; set; }

        public string ImagePath { get; set; }

        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
    }
}
