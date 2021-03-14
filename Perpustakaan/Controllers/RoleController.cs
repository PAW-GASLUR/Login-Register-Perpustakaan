using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Controllers
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class ini dapat dapat membuat dan menampilkan Index
    /// </remarks>
    public class RoleController : Controller
    {
        /// <summary>
        /// Class role controller
        /// </summary>
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
      //  [Authorize(Policy = "readonlypolicy")]
      /// <summary>
      /// Function untuk GET data role
      /// </summary>
      /// <returns>
      /// data role
      /// </returns>
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

      //  [Authorize(Policy = "writepolicy")]
      /// <summary>
      /// Function menambahkan index
      /// </summary>
      /// <returns>
      /// data Index
      /// </returns>
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        [HttpPost]
        ///<summary>
        ///Function untuk membuat role
        ///</summary> 
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

    }
}
