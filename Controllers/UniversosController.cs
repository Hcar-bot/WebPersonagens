using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPersonagens.Data;
using WebPersonagens.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebPersonagens.Controllers
{
    [Authorize]
    public class UniversosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniversosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Universos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universos.ToListAsync());
        }

        // GET: Universos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universo = await _context.Universos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universo == null)
            {
                return NotFound();
            }

            return View(universo);
        }

        // GET: Universos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Universos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] Universo universo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universo);
        }

        // GET: Universos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universo = await _context.Universos.FindAsync(id);
            if (universo == null)
            {
                return NotFound();
            }
            return View(universo);
        }

        // POST: Universos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] Universo universo)
        {
            if (id != universo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversoExists(universo.Id))
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
            return View(universo);
        }

        // GET: Universos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universo = await _context.Universos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (universo == null)
            {
                return NotFound();
            }

            return View(universo);
        }

        // POST: Universos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universo = await _context.Universos.FindAsync(id);
            if (universo != null)
            {
                _context.Universos.Remove(universo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversoExists(int id)
        {
            return _context.Universos.Any(e => e.Id == id);
        }
    }
}
