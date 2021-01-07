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
    public class RaksController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        public RaksController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "readonlypolicy")]
        // GET: Raks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rak.ToListAsync());
        }

        // GET: Raks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rak = await _context.Rak
                .FirstOrDefaultAsync(m => m.NoRak == id);
            if (rak == null)
            {
                return NotFound();
            }

            return View(rak);
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Raks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Raks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoRak,NamaRak")] Rak rak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rak);
        }
        [Authorize(Policy = "editpolicy")]
        // GET: Raks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rak = await _context.Rak.FindAsync(id);
            if (rak == null)
            {
                return NotFound();
            }
            return View(rak);
        }

        // POST: Raks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoRak,NamaRak")] Rak rak)
        {
            if (id != rak.NoRak)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RakExists(rak.NoRak))
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
            return View(rak);
        }
        [Authorize(Policy = "deletepolicy")]
        // GET: Raks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rak = await _context.Rak
                .FirstOrDefaultAsync(m => m.NoRak == id);
            if (rak == null)
            {
                return NotFound();
            }

            return View(rak);
        }

        // POST: Raks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rak = await _context.Rak.FindAsync(id);
            _context.Rak.Remove(rak);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RakExists(int id)
        {
            return _context.Rak.Any(e => e.NoRak == id);
        }
    }
}
