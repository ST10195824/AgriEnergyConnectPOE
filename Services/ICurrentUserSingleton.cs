using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Services
{
    public interface ICurrentUserSingleton
    {
        ApplicationUser CurrentUser { get; }
    }
}
