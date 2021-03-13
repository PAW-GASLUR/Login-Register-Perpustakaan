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
    /// Class ini dapat melakukan CRUD pada katalog buku
    /// </remarks>
    public class KatalogBukusController : Controller
    {
        /// <summary>
        /// Class controller katalog buku
        /// </summary>
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Menentukan data perpustakaan dapat dibaca
        /// </summary>
        /// <param name="context"></param>

        public KatalogBukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "readonlypolicy")]
        /// <summary>
        /// Function untuk get data katalog buku
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.KatalogBuku.ToListAsync());
        }

        /// <summary>
        /// Function untuk GET detail katalog buku
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <returns>
        /// data detail katalog buku
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katalogBuku = await _context.KatalogBuku
                .FirstOrDefaultAsync(m => m.NoKatalog == id);
            if (katalogBuku == null)
            {
                return NotFound();
            }

            return View(katalogBuku);
        }
        [Authorize(Policy = "writepolicy")]

        ///<summary>
        ///Function untuk membuat katalog buku
        ///</summary>
        public IActionResult Create()
        {
            return View();
        }

        // POST: KatalogBukus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoKatalog,JenisKatalog")] KatalogBuku katalogBuku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(katalogBuku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(katalogBuku);
        }
        [Authorize(Policy = "editpolicy")]

        ///<summary>
        /// Function untuk mengubah data katalog buku
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katalogBuku = await _context.KatalogBuku.FindAsync(id);
            if (katalogBuku == null)
            {
                return NotFound();
            }
            return View(katalogBuku);
        }

        /// <summary>
        /// Function untuk mengubah nomor katalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="katalogBuku"></param>
        /// <returns>
        /// perubahan nomor katalog yang diingankan oleh user
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoKatalog,JenisKatalog")] KatalogBuku katalogBuku)
        {
            if (id != katalogBuku.NoKatalog)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(katalogBuku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KatalogBukuExists(katalogBuku.NoKatalog))
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
            return View(katalogBuku);
        }
        [Authorize(Policy = "deletepolicy")]

        ///<summary>
        ///Function untuk menghapus katalog buku
        ///</summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katalogBuku = await _context.KatalogBuku
                .FirstOrDefaultAsync(m => m.NoKatalog == id);
            if (katalogBuku == null)
            {
                return NotFound();
            }

            return View(katalogBuku);
        }

        /// <summary>
        /// Funvtion untuk menghapus nomor katalog buku
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// penghapusan nomor katalog buku
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var katalogBuku = await _context.KatalogBuku.FindAsync(id);
            _context.KatalogBuku.Remove(katalogBuku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KatalogBukuExists(int id)
        {
            return _context.KatalogBuku.Any(e => e.NoKatalog == id);
        }
    }
}
