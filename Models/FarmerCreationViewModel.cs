using AgriEnergyConnectPOE.Data;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectPOE.Models
{
    public class FarmerCreationViewModel
    {

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "FarmersProducts")]
        public List<Product> FarmersProducts { get; set; }

    }
}
