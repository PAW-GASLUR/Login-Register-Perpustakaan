﻿using System;
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
    /// Class ini dapat melakukan CRUD pada Mahasiswa
    /// </remarks>
    public class MahasiswasController : Controller
    {
        private readonly PERPUSTAKAAN_PAWContext _context;

        /// <summary>
        /// Class Controller Mahasiswa
        /// </summary>
        /// <param name="context"></param>
        public MahasiswasController(PERPUSTAKAAN_PAWContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Function untuk GET Mahasiswa
        /// </summary>
        /// <param name="ktsd">parameter ketersediaan</param>
        /// <param name="searchString">parameter pencarian</param>
        /// <returns>data index mahasiswa</returns>
        [Authorize(Policy = "readonlypolicy")]
        // GET: Mahasiswas
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.Mahasiswa orderby d.Nama select d.Nama;

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Mahasiswa.Include(k => k.NoGenderNavigation) select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.Nama == ktsd);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.Nim.Contains(searchString) || s.Nama.Contains(searchString));
            }

            return View(await menu.ToListAsync());
            //var pERPUSTAKAAN_PAWContext = _context.Mahasiswa.Include(m => m.NoGenderNavigation);
            //return View(await pERPUSTAKAAN_PAWContext.ToListAsync());
        }

        // GET: Mahasiswas/Details/5
        /// <summary>
        /// Function untuk GET detail Mahasiswa
        /// </summary>
        /// <param name="id">parameter ID</param>
        /// <returns>
        /// data detail mahasiswa
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mahasiswa = await _context.Mahasiswa
                .Include(m => m.NoGenderNavigation)
                .FirstOrDefaultAsync(m => m.NoAnggota == id);
            if (mahasiswa == null)
            {
                return NotFound();
            }

            return View(mahasiswa);
        }
        /// <summary>
        /// Function untuk GET membuat Mahasiswa
        /// </summary>
        /// <returns>data create mahasiswa</returns>
        [Authorize(Policy = "writepolicy")]
        // GET: Mahasiswas/Create
        public IActionResult Create()
        {
            ViewData["NoGender"] = new SelectList(_context.Gender, "NoGender", "NamaGender");
            return View();
        }

        // POST: Mahasiswas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST membuat Mahasiswa
        /// </summary>
        /// <param name="mahasiswa"></param>
        /// <returns>data create mahasiswa</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoAnggota,Nim,Nama,NoGender,NoHp,Alamat")] Mahasiswa mahasiswa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mahasiswa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NoGender"] = new SelectList(_context.Gender, "NoGender", "NamaGender", mahasiswa.NoGender);
            return View(mahasiswa);
        }
        /// <summary>
        /// Function untuk GET mahasiswa yang akan di edit
        /// </summary>
        /// <param name="id">parameter id</param>
        /// <returns>perubahan data mahasiswa</returns>
        [Authorize(Policy = "editpolicy")]
        // GET: Mahasiswas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mahasiswa = await _context.Mahasiswa.FindAsync(id);
            if (mahasiswa == null)
            {
                return NotFound();
            }
            ViewData["NoGender"] = new SelectList(_context.Gender, "NoGender", "NamaGender", mahasiswa.NoGender);
            return View(mahasiswa);
        }

        // POST: Mahasiswas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Function untuk POST Mahasiswa yang akan diedit
        /// </summary>
        /// <param name="id">parameter id</param>
        /// <param name="mahasiswa">parameter mahasiswa</param>
        /// <returns>
        /// perubahan data mahasiswa
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoAnggota,Nim,Nama,NoGender,NoHp,Alamat")] Mahasiswa mahasiswa)
        {
            if (id != mahasiswa.NoAnggota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mahasiswa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MahasiswaExists(mahasiswa.NoAnggota))
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
            ViewData["NoGender"] = new SelectList(_context.Gender, "NoGender", "NamaGender", mahasiswa.NoGender);
            return View(mahasiswa);
        }
        /// <summary>
        /// Function untuk GET Mahasiswa yang akan dihapus
        /// </summary>
        /// <param name="id"></param>
        /// <returns>pernghapusan data mahasiswa</returns>
        [Authorize(Policy = "deletepolicy")]
        // GET: Mahasiswas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mahasiswa = await _context.Mahasiswa
                .Include(m => m.NoGenderNavigation)
                .FirstOrDefaultAsync(m => m.NoAnggota == id);
            if (mahasiswa == null)
            {
                return NotFound();
            }

            return View(mahasiswa);
        }

        // POST: Mahasiswas/Delete/5
        /// <summary>
        /// Function untuk POST Mahasiswa yang akan dihapus
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// penghapusan data Mahasiswa
        /// </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mahasiswa = await _context.Mahasiswa.FindAsync(id);
            _context.Mahasiswa.Remove(mahasiswa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MahasiswaExists(int id)
        {
            return _context.Mahasiswa.Any(e => e.NoAnggota == id);
        }
    }
}
