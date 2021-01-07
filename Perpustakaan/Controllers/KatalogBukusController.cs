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
    public class KatalogBukusController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        public KatalogBukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "readonlypolicy")]
        // GET: KatalogBukus
        public async Task<IActionResult> Index()
        {
            return View(await _context.KatalogBuku.ToListAsync());
        }

        // GET: KatalogBukus/Details/5
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
        // GET: KatalogBukus/Create
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
        // GET: KatalogBukus/Edit/5
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

        // POST: KatalogBukus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // GET: KatalogBukus/Delete/5
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

        // POST: KatalogBukus/Delete/5
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
