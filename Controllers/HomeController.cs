using AgriEnergyConnectPOE.Data;
using AgriEnergyConnectPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgriEnergyConnectPOE.Controllers
{
    //------------------------------------------------------------------------------------//
    //                          *Home Controller*                                         //
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Controller for managing the home page and error handling.
    /// </summary>
    public class HomeController: Controller
    {
        //----------------------------------------------------------//
        //                          *Fields*                        //
        //----------------------------------------------------------//
        private readonly ILogger<HomeController> _logger;

        //-------------------------------*Constructor*-------------------------------//
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //-------------------------------*Home Page*-------------------------------//
        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The view containing the home page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        //-------------------------------*Error Page*-------------------------------//
        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The view containing the error details.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
//----------------------------------------------------End_of_File----------------------------------------------------//
