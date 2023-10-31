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
    public class ContasController : Controller
    {
        private readonly ProsperaContext _context;

        public ContasController(ProsperaContext context)
        {
            _context = context;
        }

        // GET: Contas
        public async Task<IActionResult> Index()
        {
            var prosperaContext = _context.Contas.Include(c => c.Usuario);
            return View(await prosperaContext.ToListAsync());
        }

        // GET: Contas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contas == null)
            {
                return NotFound();
            }

            var contas = await _context.Contas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdContas == id);
            if (contas == null)
            {
                return NotFound();
            }

            return View(contas);
        }

        // GET: Contas/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Set<Usuario>(), "IdUsuario", "EmailUsuario");
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContas,CodigoCont,TipoCont,DatEmissaoCont,DatVenciCont,DevedorCont,PagadorCont,Descricaocont,ValorCont,StatusCont,MetodoPgtoCont,ObservacaoCont,IdUsuario")] Contas contas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Set<Usuario>(), "IdUsuario", "EmailUsuario", contas.IdUsuario);
            return View(contas);
        }

        // GET: Contas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contas == null)
            {
                return NotFound();
            }

            var contas = await _context.Contas.FindAsync(id);
            if (contas == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Set<Usuario>(), "IdUsuario", "EmailUsuario", contas.IdUsuario);
            return View(contas);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContas,CodigoCont,TipoCont,DatEmissaoCont,DatVenciCont,DevedorCont,PagadorCont,Descricaocont,ValorCont,StatusCont,MetodoPgtoCont,ObservacaoCont,IdUsuario")] Contas contas)
        {
            if (id != contas.IdContas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContasExists(contas.IdContas))
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
            ViewData["IdUsuario"] = new SelectList(_context.Set<Usuario>(), "IdUsuario", "EmailUsuario", contas.IdUsuario);
            return View(contas);
        }

        // GET: Contas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contas == null)
            {
                return NotFound();
            }

            var contas = await _context.Contas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdContas == id);
            if (contas == null)
            {
                return NotFound();
            }

            return View(contas);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contas == null)
            {
                return Problem("Entity set 'ProsperaContext.Contas'  is null.");
            }
            var contas = await _context.Contas.FindAsync(id);
            if (contas != null)
            {
                _context.Contas.Remove(contas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContasExists(int id)
        {
          return (_context.Contas?.Any(e => e.IdContas == id)).GetValueOrDefault();
        }
    }
}
