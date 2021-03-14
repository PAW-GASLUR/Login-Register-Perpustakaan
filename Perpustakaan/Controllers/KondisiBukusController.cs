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
{/// <summary>
/// Main Class
/// </summary>
/// <remarks>
/// Class ini dapat melakukan CRUD pada Kondisi Buku
/// </remarks>
    public class KondisiBukusController : Controller
    {/// <summary>
    /// Class Controller Kondisi Buku
    /// </summary>
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Menentukan data perpustakaan dapat dibaca
        /// </summary>
        /// <param name="context"></param>
        public KondisiBukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Function untuk GET data kondisi buku
        /// </summary>
        /// <returns>data index kondisi buku</returns>
        [Authorize(Policy = "readonlypolicy")]
        // GET: KondisiBukus
        public async Task<IActionResult> Index()
        {
            return View(await _context.KondisiBuku.ToListAsync());
        }

        // GET: KondisiBukus/Details/5
        /// <summary>
        /// FUnction untuk GET detail kondisi buku
        /// </summary>
        /// <param name="id">parameter Id</param>
        /// <returns>
        /// data detail kondisi buku
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiBuku = await _context.KondisiBuku
                .FirstOrDefaultAsync(m => m.NoKondisi == id);
            if (kondisiBuku == null)
            {
                return NotFound();
            }

            return View(kondisiBuku);
        }
        /// <summary>
        /// Function untuk GET kondisi buku yang akan dibuat
        /// </summary>
        /// <returns>data create kondisi buku</returns>
        [Authorize(Policy = "writepolicy")]
        // GET: KondisiBukus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KondisiBukus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST Kondisi buku yang akan dibuat
        /// </summary>
        /// <param name="kondisiBuku">parameter Kondisi Buku</param>
        /// <returns>data create kondisi buku</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoKondisi,NamaKondisi")] KondisiBuku kondisiBuku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kondisiBuku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kondisiBuku);
        }
        /// <summary>
        /// Function untuk GET kondisi buku yang akan diubha
        /// </summary>
        /// <param name="id"></param>
        /// <returns>data perubahan kondisi buku</returns>
        [Authorize(Policy = "editpolicy")]
        // GET: KondisiBukus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiBuku = await _context.KondisiBuku.FindAsync(id);
            if (kondisiBuku == null)
            {
                return NotFound();
            }
            return View(kondisiBuku);
        }

        // POST: KondisiBukus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST Kondisi buku yang akan diubah
        /// </summary>
        /// <param name="id">Parameter ID</param>
        /// <param name="kondisiBuku">Parameter Kondisi Buku</param>
        /// <returns>data perubahan nomor kondisi buku dan nama kondisi buku</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoKondisi,NamaKondisi")] KondisiBuku kondisiBuku)
        {
            if (id != kondisiBuku.NoKondisi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kondisiBuku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KondisiBukuExists(kondisiBuku.NoKondisi))
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
            return View(kondisiBuku);
        }
        /// <summary>
        /// Function untuk GET Kondisi buku yang akan dihapus
        /// </summary>
        /// <param name="id"></param>
        /// <returns>penghapusan data kondisi buku</returns>
        [Authorize(Policy = "deletepolicy")]
        // GET: KondisiBukus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiBuku = await _context.KondisiBuku
                .FirstOrDefaultAsync(m => m.NoKondisi == id);
            if (kondisiBuku == null)
            {
                return NotFound();
            }

            return View(kondisiBuku);
        }

        // POST: KondisiBukus/Delete/5
        /// <summary>
        /// Function untuk POST Kondisi buku yang akan dihapus
        /// </summary>
        /// <param name="id">parameter id</param>
        /// <returns>
        /// Menghapus kondisi buku
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kondisiBuku = await _context.KondisiBuku.FindAsync(id);
            _context.KondisiBuku.Remove(kondisiBuku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KondisiBukuExists(int id)
        {
            return _context.KondisiBuku.Any(e => e.NoKondisi == id);
        }
    }
}
