using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectPOE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICurrentUserSingleton, CurrentUserSingleton>();
            builder.Services.AddScoped<ImageService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();


            // seeding the Roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Farmer", "Employee" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //Seeding the Admin Employee
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = new ApplicationUser();
                var email = "Admin@admin.com";
                var password = "Admin123@";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    adminUser.Email = email;
                    adminUser.FirstName = "Big";
                    adminUser.Surname = "Guy";
                    adminUser.UserName = email;

                    var result = await userManager.CreateAsync(adminUser, password);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine("IT DIDNT CREATE A USER");
                    }

                    await userManager.AddToRoleAsync(adminUser, "Employee");

                }
                
            }

            app.Run();
        }
    }
}
