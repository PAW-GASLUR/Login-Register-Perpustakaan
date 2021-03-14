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
    /// <remask>
    /// Class ini dapat melakukan CRUD pada Peminjaman
    /// </remask>
    public class PeminjamenController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Class Controller Peminjaman
        /// </summary>
        /// <param name="context"></param>
        public PeminjamenController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }

        // GET: Peminjamen
        /// <summary>
        /// Function untuk GET data Peminjaman
        /// </summary>
        /// <param name="ktsd">Parameter ketersediaan</param>
        /// <param name="searchString">Parameter pencarian</param>
        /// <returns></returns>
        [Authorize(Policy="readonlypolicy")]
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.Peminjaman orderby d.NoAnggotaNavigation.Nama select d.NoAnggotaNavigation.Nama;

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Peminjaman.Include(k => k.NoBukuNavigation).Include(k => k.NoAnggotaNavigation) select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NoAnggotaNavigation.Nama == ktsd);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.TglPeminjaman.ToString().Contains(searchString) || s.NoAnggotaNavigation.Nama.Contains(searchString) || s.NoBukuNavigation.JudulBuku.Contains(searchString));
            }

            return View(await menu.ToListAsync());
            //var pERPUSTAKAAN_PAWContext = _context.Peminjaman.Include(p => p.NoAnggotaNavigation).Include(p => p.NoBukuNavigation);
            //return View(await pERPUSTAKAAN_PAWContext.ToListAsync());
        }

        // GET: Peminjamen/Details/5
        /// <summary>
        /// Function GET detail peminjaman
        /// </summary>
        /// <param name="id">Paramter Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjaman
                .Include(p => p.NoAnggotaNavigation)
                .Include(p => p.NoBukuNavigation)
                .FirstOrDefaultAsync(m => m.NoPeminjaman == id);
            if (peminjaman == null)
            {
                return NotFound();
            }

            return View(peminjaman);
        }

        /// <summary>
        /// Function untuk GET peminjaman yang akan dibuat
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "writepolicy")]
        // GET: Peminjamen/Create
        public IActionResult Create()
        {
            ViewData["NoAnggota"] = new SelectList(_context.Mahasiswa, "NoAnggota", "Nama");
            ViewData["NoBuku"] = new SelectList(_context.Buku, "NoBuku", "JudulBuku");
            return View();
        }

        // POST: Peminjamen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Funtion untuk POST peminjaman yang akan dibuat
        /// </summary>
        /// <param name="peminjaman"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoPeminjaman,TglPeminjaman,NoBuku,NoAnggota")] Peminjaman peminjaman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peminjaman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoAnggota"] = new SelectList(_context.Mahasiswa, "NoAnggota", "Nama", peminjaman.NoAnggota);
            ViewData["NoBuku"] = new SelectList(_context.Buku, "NoBuku", "JudulBuku", peminjaman.NoBuku);
            return View(peminjaman);
        }

        /// <summary>
        /// Function untuk GET peminjaman yang akan diedit
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <returns></returns>
        [Authorize(Policy = "editpolicy")]
        // GET: Peminjamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjaman.FindAsync(id);
            if (peminjaman == null)
            {
                return NotFound();
            }
            ViewData["NoAnggota"] = new SelectList(_context.Mahasiswa, "NoAnggota", "Nama", peminjaman.NoAnggota);
            ViewData["NoBuku"] = new SelectList(_context.Buku, "NoBuku", "JudulBuku", peminjaman.NoBuku);
            return View(peminjaman);
        }

        // POST: Peminjamen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST peminjaman yang akan diedit
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <param name="peminjaman">Parameter Peminjaman</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoPeminjaman,TglPeminjaman,NoBuku,NoAnggota")] Peminjaman peminjaman)
        {
            if (id != peminjaman.NoPeminjaman)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peminjaman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeminjamanExists(peminjaman.NoPeminjaman))
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
            ViewData["NoAnggota"] = new SelectList(_context.Mahasiswa, "NoAnggota", "Nama", peminjaman.NoAnggota);
            ViewData["NoBuku"] = new SelectList(_context.Buku, "NoBuku", "JudulBuku", peminjaman.NoBuku);
            return View(peminjaman);
        }

        /// <summary>
        /// Function untuk GET peminjamnan yang akan dihapus
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <returns></returns>
        [Authorize(Policy = "deletepolicy")]
        // GET: Peminjamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjaman
                .Include(p => p.NoAnggotaNavigation)
                .Include(p => p.NoBukuNavigation)
                .FirstOrDefaultAsync(m => m.NoPeminjaman == id);
            if (peminjaman == null)
            {
                return NotFound();
            }

            return View(peminjaman);
        }

        // POST: Peminjamen/Delete/5
        /// <summary>
        /// Function untuk POST peminjaman yang akan dihapus
        /// </summary>
        /// <param name="id">Parameter id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peminjaman = await _context.Peminjaman.FindAsync(id);
            _context.Peminjaman.Remove(peminjaman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeminjamanExists(int id)
        {
            return _context.Peminjaman.Any(e => e.NoPeminjaman == id);
        }
    }
}
