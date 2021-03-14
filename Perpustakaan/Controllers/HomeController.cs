using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
 
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class ini dapat menampilkan keterangan dibagian home 
    /// </remarks>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// function menampilkan Index
        /// </summary>
        /// <returns>
        /// data Index
        /// </returns>

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        /// <summary>
        /// function menampilkan About
        /// </summary>
        /// <returns>
        /// data About 
        /// </returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        /// <summary>
        /// function menampikan Contact
        /// </summary>
        /// <returns>
        /// data contact
        /// </returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// function menampilkan privacy
        /// </summary>
        /// <returns>
        /// data privacy
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        ///<summary>
        /// function menampilkan error
        ///</summary>
    }
}
