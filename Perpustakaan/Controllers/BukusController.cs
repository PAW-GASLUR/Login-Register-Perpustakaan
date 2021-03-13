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
    /// Class ini dapat melakukan CRUD pada Buku.
    /// </remarks>
    public class BukusController : Controller
    {
        /// <summary>
        /// Class controller buku
        /// </summary>
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Menentukan data perpustakaan dapat dibaca.
        /// </summary>
        /// <param name="context"></param>
        public BukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        [Authorize(Policy="readonlypolicy")]
        
        /// <summary>
        /// Function ntuk GET data buku
        /// </summary>
        /// <param name="ktsd">Parameter ketersediaan</param>
        /// <param name="searchString">Parameter pencarian</param>
        // GET: Bukus
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Buku orderby d.JudulBuku select d.JudulBuku;
            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Buku.Include(k => k.NoKatalogNavigation).Include(k => k.NoRakNavigation) select m;
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.JudulBuku == ktsd);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.JudulBuku.Contains(searchString) || s.Pengarang.Contains(searchString));
            }
            return View(await menu.ToListAsync());
        }



        /// <summary>
        /// Function untuk GET detail buku
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <returns></returns>
        // GET: Bukus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .Include(b => b.NoKatalogNavigation)
                .Include(b => b.NoRakNavigation)
                .FirstOrDefaultAsync(m => m.NoBuku == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }
        [Authorize(Policy = "writepolicy")]

        /// <summary>
        /// Function untuk GET buku yang akan dibuat
        /// </summary>
        // GET: Bukus/Create
        public IActionResult Create()
        {
            ViewData["NoKatalog"] = new SelectList(_context.KatalogBuku, "NoKatalog", "JenisKatalog");
            ViewData["NoRak"] = new SelectList(_context.Rak, "NoRak", "NamaRak");
            return View();
        }

        /// <summary>
        /// Function untuk POST buku yang akan dibuat
        /// </summary>
        /// <param name="buku">Parameter buku</param>
        /// <returns></returns>
        // POST: Bukus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoBuku,JudulBuku,NoKatalog,NoRak,Pengarang,Penerbit")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoKatalog"] = new SelectList(_context.KatalogBuku, "NoKatalog", "NoKatalog", buku.NoKatalog);
            ViewData["NoRak"] = new SelectList(_context.Rak, "NoRak", "NoRak", buku.NoRak);
            return View(buku);
        }
        [Authorize(Policy = "editpolicy")]

        /// <summary>
        /// Function untuk GET buku yang akan diedit
        /// </summary>
        // GET: Bukus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku.FindAsync(id);
            if (buku == null)
            {
                return NotFound();
            }
            ViewData["NoKatalog"] = new SelectList(_context.KatalogBuku, "NoKatalog", "JenisKatalog", buku.NoKatalog);
            ViewData["NoRak"] = new SelectList(_context.Rak, "NoRak", "NamaRak", buku.NoRak);
            return View(buku);
        }

        /// <summary>
        /// Function untuk POST buku yang akan diedit
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <param name="buku">Parameter buku</param>
        /// <returns></returns>
        // POST: Bukus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoBuku,JudulBuku,NoKatalog,NoRak,Pengarang,Penerbit")] Buku buku)
        {
            if (id != buku.NoBuku)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BukuExists(buku.NoBuku))
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
            ViewData["NoKatalog"] = new SelectList(_context.KatalogBuku, "NoKatalog", "JenisKatalog", buku.NoKatalog);
            ViewData["NoRak"] = new SelectList(_context.Rak, "NoRak", "NamaRak", buku.NoRak);
            return View(buku);
        }
        [Authorize(Policy = "deletepolicy")]

        /// <summary>
        /// Function untuk GET buku yang akan dihapus
        /// </summary>
        // GET: Bukus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .Include(b => b.NoKatalogNavigation)
                .Include(b => b.NoRakNavigation)
                .FirstOrDefaultAsync(m => m.NoBuku == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }
        /// <summary>
        /// Function untuk POST buku yang akan dihapus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Bukus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buku = await _context.Buku.FindAsync(id);
            _context.Buku.Remove(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BukuExists(int id)
        {
            return _context.Buku.Any(e => e.NoBuku == id);
        }
    }
}
