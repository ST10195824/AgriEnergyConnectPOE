using AgriEnergyConnectPOE.Data;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE.Services
{
    public class CurrentUserSingleton: ICurrentUserSingleton
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserSingleton(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public ApplicationUser? CurrentUser
        {
            get
            {
                return (ApplicationUser?)(_context.Users?.FirstOrDefault(predicate: u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name));
            }
        }
    }
}
