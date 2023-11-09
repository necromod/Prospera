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
    public class ContaBancariasController : Controller
    {
        private readonly ProsperaContext _context;

        public ContaBancariasController(ProsperaContext context)
        {
            _context = context;
        }

        // GET: ContaBancarias
        public async Task<IActionResult> Index()
        {
            var prosperaContext = _context.ContaBancaria.Include(c => c.Terceiros).Include(c => c.Usuario);
            return View(await prosperaContext.ToListAsync());
        }

        public IActionResult ConsultaContasBancarias() 
        {
            return View();
        }

        public IActionResult CreateContasBancarias()
        {
            return View();
        }
        // GET: ContaBancarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContaBancaria == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.ContaBancaria
                .Include(c => c.Terceiros)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdContaBancaria == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            return View(contaBancaria);
        }

        // GET: ContaBancarias/Create
        public IActionResult Create()
        {
            ViewData["IdTerceiros"] = new SelectList(_context.Terceiros, "IdTerceiros", "NomeTerceiros");
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario");
            return View();
        }

        // POST: ContaBancarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContaBancaria,TitularContBan,NumContBan,AgenciaContBan,TipoContBan,SaldoContBan,ObsContBan,IdUsuario,IdTerceiros")] ContaBancaria contaBancaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaBancaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTerceiros"] = new SelectList(_context.Terceiros, "IdTerceiros", "NomeTerceiros", contaBancaria.IdTerceiros);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contaBancaria.IdUsuario);
            return View(contaBancaria);
        }

        // GET: ContaBancarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContaBancaria == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.ContaBancaria.FindAsync(id);
            if (contaBancaria == null)
            {
                return NotFound();
            }
            ViewData["IdTerceiros"] = new SelectList(_context.Terceiros, "IdTerceiros", "NomeTerceiros", contaBancaria.IdTerceiros);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contaBancaria.IdUsuario);
            return View(contaBancaria);
        }

        // POST: ContaBancarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContaBancaria,TitularContBan,NumContBan,AgenciaContBan,TipoContBan,SaldoContBan,ObsContBan,IdUsuario,IdTerceiros")] ContaBancaria contaBancaria)
        {
            if (id != contaBancaria.IdContaBancaria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaBancaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaBancariaExists(contaBancaria.IdContaBancaria))
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
            ViewData["IdTerceiros"] = new SelectList(_context.Terceiros, "IdTerceiros", "NomeTerceiros", contaBancaria.IdTerceiros);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contaBancaria.IdUsuario);
            return View(contaBancaria);
        }

        // GET: ContaBancarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContaBancaria == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.ContaBancaria
                .Include(c => c.Terceiros)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdContaBancaria == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            return View(contaBancaria);
        }

        // POST: ContaBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContaBancaria == null)
            {
                return Problem("Entity set 'ProsperaContext.ContaBancaria'  is null.");
            }
            var contaBancaria = await _context.ContaBancaria.FindAsync(id);
            if (contaBancaria != null)
            {
                _context.ContaBancaria.Remove(contaBancaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaBancariaExists(int id)
        {
          return (_context.ContaBancaria?.Any(e => e.IdContaBancaria == id)).GetValueOrDefault();
        }
    }
}
