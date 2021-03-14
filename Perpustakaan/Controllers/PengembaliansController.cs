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
    /// Class ini dapat melakukan CRUD pada Pengembalian
    /// </remarks>
    public class PengembaliansController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Class Controller Pengembalian
        /// </summary>
        /// <param name="context"></param>
        public PengembaliansController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Function untuk GET data Pengembalian
        /// </summary>
        /// <param name="ktsd"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = "readonlypolicy")]

        // GET: Pengembalians
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.Pengembalian orderby d.NoPeminjaman.ToString() select d.NoPeminjaman.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Pengembalian.Include(k => k.NoKondisiNavigation).Include(k => k.NoPeminjamanNavigation) select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NoPeminjaman.ToString() == ktsd);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NoPeminjaman.ToString().Contains(searchString));
            }

            return View(await menu.ToListAsync());
            //var pERPUSTAKAAN_PAWContext = _context.Pengembalian.Include(p => p.NoKondisiNavigation).Include(p => p.NoPeminjamanNavigation);
            //return View(await pERPUSTAKAAN_PAWContext.ToListAsync());
        }

        // GET: Pengembalians/Details/5
        /// <summary>
        /// Function untuk GET detail pengembalian
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian = await _context.Pengembalian
                .Include(p => p.NoKondisiNavigation)
                .Include(p => p.NoPeminjamanNavigation)
                .FirstOrDefaultAsync(m => m.NoPengembalian == id);
            if (pengembalian == null)
            {
                return NotFound();
            }

            return View(pengembalian);
        }
        /// <summary>
        /// Function untuk GET Pengembalian yang akan dibuat
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "writepolicy")]
        // GET: Pengembalians/Create
        public IActionResult Create()
        {
            ViewData["NoKondisi"] = new SelectList(_context.KondisiBuku, "NoKondisi", "NamaKondisi");
            ViewData["NoPeminjaman"] = new SelectList(_context.Peminjaman, "NoPeminjaman", "NoPeminjaman");
            return View();
        }

        // POST: Pengembalians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST pengembalian yang akan dibuat
        /// </summary>
        /// <param name="pengembalian">Parameter pengembalian</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoPengembalian,TglPengembalian,NoKondisi,Denda,NoPeminjaman")] Pengembalian pengembalian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pengembalian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoKondisi"] = new SelectList(_context.KondisiBuku, "NoKondisi", "NamaKondisi", pengembalian.NoKondisi);
            ViewData["NoPeminjaman"] = new SelectList(_context.Peminjaman, "NoPeminjaman", "NoPeminjaman", pengembalian.NoPeminjaman);
            return View(pengembalian);
        }
        /// <summary>
        /// Function untuk GET peminjaman yang akan diedit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "editpolicy")]
        // GET: Pengembalians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian = await _context.Pengembalian.FindAsync(id);
            if (pengembalian == null)
            {
                return NotFound();
            }
            ViewData["NoKondisi"] = new SelectList(_context.KondisiBuku, "NoKondisi", "NamaKondisi", pengembalian.NoKondisi);
            ViewData["NoPeminjaman"] = new SelectList(_context.Peminjaman, "NoPeminjaman", "NoPeminjaman", pengembalian.NoPeminjaman);
            return View(pengembalian);
        }

        // POST: Pengembalians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST pengembalian yang akan diedit
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <param name="pengembalian">Parameter Pengembalian</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoPengembalian,TglPengembalian,NoKondisi,Denda,NoPeminjaman")] Pengembalian pengembalian)
        {
            if (id != pengembalian.NoPengembalian)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pengembalian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PengembalianExists(pengembalian.NoPengembalian))
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
            ViewData["NoKondisi"] = new SelectList(_context.KondisiBuku, "NoKondisi", "NamaKondisi", pengembalian.NoKondisi);
            ViewData["NoPeminjaman"] = new SelectList(_context.Peminjaman, "NoPeminjaman", "NoPeminjaman", pengembalian.NoPeminjaman);
            return View(pengembalian);
        }
        /// <summary>
        /// Function untuk GET pengembalian yang akan dihapus
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <returns></returns>
        [Authorize(Policy = "deletepolicy")]
        // GET: Pengembalians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian = await _context.Pengembalian
                .Include(p => p.NoKondisiNavigation)
                .Include(p => p.NoPeminjamanNavigation)
                .FirstOrDefaultAsync(m => m.NoPengembalian == id);
            if (pengembalian == null)
            {
                return NotFound();
            }

            return View(pengembalian);
        }

        // POST: Pengembalians/Delete/5
        /// <summary>
        /// Function untuk POST pengembalian yang akan dihapus
        /// </summary>
        /// <param name="id">Parameter Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pengembalian = await _context.Pengembalian.FindAsync(id);
            _context.Pengembalian.Remove(pengembalian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PengembalianExists(int id)
        {
            return _context.Pengembalian.Any(e => e.NoPengembalian == id);
        }
    }
}
