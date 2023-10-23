using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class ExtratoController : Controller
    {
        private readonly ProsperaContext _context;

        public ExtratoController(ProsperaContext context)
        {
            _context = context;
        }

        // GET: Extrato
        public async Task<IActionResult> Index()
        {
            var prosperaContext = _context.Extrato.Include(e => e.Usuario);
            return View(await prosperaContext.ToListAsync());
        }

        // GET: Extrato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Extrato == null)
            {
                return NotFound();
            }

            var extrato = await _context.Extrato
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.IdExtrato == id);
            if (extrato == null)
            {
                return NotFound();
            }

            return View(extrato);
        }

        // GET: Extrato/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "EmailUsuario");
            return View();
        }

        // POST: Extrato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExtrato,NomeExtrato,ValorExtrato,DestinatarioExtrato,RemetenteExtrato,DataExtrato,StatusExtrato,ObservacaoExtrato,IdUsuario")] Extrato extrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "EmailUsuario", extrato.IdUsuario);
            return View(extrato);
        }

        // GET: Extrato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Extrato == null)
            {
                return NotFound();
            }

            var extrato = await _context.Extrato.FindAsync(id);
            if (extrato == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "EmailUsuario", extrato.IdUsuario);
            return View(extrato);
        }

        // POST: Extrato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExtrato,NomeExtrato,ValorExtrato,DestinatarioExtrato,RemetenteExtrato,DataExtrato,StatusExtrato,ObservacaoExtrato,IdUsuario")] Extrato extrato)
        {
            if (id != extrato.IdExtrato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtratoExists(extrato.IdExtrato))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "EmailUsuario", extrato.IdUsuario);
            return View(extrato);
        }

        // GET: Extrato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Extrato == null)
            {
                return NotFound();
            }

            var extrato = await _context.Extrato
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.IdExtrato == id);
            if (extrato == null)
            {
                return NotFound();
            }

            return View(extrato);
        }

        // POST: Extrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Extrato == null)
            {
                return Problem("Entity set 'ProsperaContext.Extrato'  is null.");
            }
            var extrato = await _context.Extrato.FindAsync(id);
            if (extrato != null)
            {
                _context.Extrato.Remove(extrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtratoExists(int id)
        {
          return (_context.Extrato?.Any(e => e.IdExtrato == id)).GetValueOrDefault();
        }
    }
}
