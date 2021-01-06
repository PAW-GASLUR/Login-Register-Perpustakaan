using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    public class KondisiBukusController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        public KondisiBukusController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }

        // GET: KondisiBukus
        public async Task<IActionResult> Index()
        {
            return View(await _context.KondisiBuku.ToListAsync());
        }

        // GET: KondisiBukus/Details/5
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

        // GET: KondisiBukus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KondisiBukus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
