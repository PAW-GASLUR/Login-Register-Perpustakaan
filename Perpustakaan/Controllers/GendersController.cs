using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class ini dapat melakukan CRUD pada gender
    /// </remarks>
    public class GendersController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Class controller gender
        /// </summary>
        /// <param name="context"></param>
        public GendersController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Function untuk GET data gender
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "readonlypolicy")]
        // GET: Genders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gender.ToListAsync());
        }

        // GET: Genders/Details/5
        /// <summary>
        /// Function untuk GET detail gender
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.NoGender == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }
        /// <summary>
        /// Function untuk GET gender yang akan dibuat
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "writepolicy")]
        // GET: Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST gender yang akan dibuat
        /// </summary>
        /// <param name="gender">Parameter Gender</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoGender,NamaGender")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }
        /// <summary>
        /// Function untuk GET gender yang akan diedit
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <returns></returns>
        [Authorize(Policy = "editpolicy")]
        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST gender yang akan diedit
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <param name="gender">Parameter Gender</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoGender,NamaGender")] Gender gender)
        {
            if (id != gender.NoGender)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.NoGender))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }
        /// <summary>
        /// Function untuk GET gender yang akan dihapus
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <returns></returns>
        [Authorize(Policy = "deletepolicy")]
        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.NoGender == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Genders/Delete/5
        /// <summary>
        ///Function POST gender yang akan dihapus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gender = await _context.Gender.FindAsync(id);
            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool GenderExists(int id)
        {
            return _context.Gender.Any(e => e.NoGender == id);
        }
        
    }
}
