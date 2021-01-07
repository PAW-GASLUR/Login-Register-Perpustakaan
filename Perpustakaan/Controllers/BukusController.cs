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
    public class BukusController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        public BukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        [Authorize(Policy="readonlypolicy")]
        // GET: Bukus
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.Buku orderby d.JudulBuku select d.JudulBuku;

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Buku.Include(k => k.NoKatalogNavigation).Include(k => k.NoRakNavigation) select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.JudulBuku == ktsd);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.JudulBuku.Contains(searchString) || s.Pengarang.Contains(searchString));
            }
            //var pERPUSTAKAAN_PAWContext = _context.Buku.Include(b => b.NoKatalogNavigation).Include(b => b.NoRakNavigation);
            //return View(await pERPUSTAKAAN_PAWContext.ToListAsync());
            return View(await menu.ToListAsync());
        }


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
        // GET: Bukus/Create
        public IActionResult Create()
        {
            ViewData["NoKatalog"] = new SelectList(_context.KatalogBuku, "NoKatalog", "JenisKatalog");
            ViewData["NoRak"] = new SelectList(_context.Rak, "NoRak", "NamaRak");
            return View();
        }

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
