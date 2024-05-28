using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnectPOE.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }

    }
}
