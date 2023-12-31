﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class TerceirosController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;

        public TerceirosController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        // GET: Terceiros
        public async Task<IActionResult> Index()
        {
            var prosperaContext = _context.Terceiros.Include(t => t.Usuario);
            return View(await prosperaContext.ToListAsync());
        }

        public IActionResult CreateTerceiros() 
        {
            return View();
       }
        public IActionResult Consulta()
        {
            var viewModel = new TerceirosViewModelInterface(_context, _sessao);
            viewModel.PreencherListaTerceiros();

            // Ordena a lista de Terceiros por CodigoCont
            viewModel.ListaTerceiros = viewModel.ListaTerceiros.OrderBy(t => t.CodigoCont).ToList();

            // Agora, viewModel.ListaTerceiros está populado com os dados da tabela Terceiros

            return View(viewModel);
        }

        public IActionResult ConsultaEndereco()
        {
            var viewModel = new TerceirosViewModelInterface(_context, _sessao);
            viewModel.PreencherListaTerceiros();

            // Ordena a lista de Terceiros por CodigoCont
            viewModel.ListaTerceiros = viewModel.ListaTerceiros.OrderBy(t => t.CodigoCont).ToList();

            // Agora, viewModel.ListaTerceiros está populado com os dados da tabela Terceiros

            return View(viewModel);
        }

        // GET: Terceiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Terceiros == null)
            {
                return NotFound();
            }

            var terceiros = await _context.Terceiros
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.IdTerceiros == id);
            if (terceiros == null)
            {
                return NotFound();
            }

            return View(terceiros);
        }

        // GET: Terceiros/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario");
            return View();
        }

        // POST: Terceiros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTerceiros,NomeTerceiros,TelefoneTerceiros,Telefone2Terceiros,EmailTerceiros,EnderecoTerceiros,CidadeTerceiros,BairroTerceiros,UFTerceiros,CEPTerceiros,ObservacaoTerceiros,DataCadastroTerceiros,DataUltimaMovimentacao,StatusTerceiros,IdUsuario")] Terceiros terceiros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terceiros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", terceiros.IdUsuario);
            return View(terceiros);
        }

        // GET: Terceiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Terceiros == null)
            {
                return NotFound();
            }

            var terceiros = await _context.Terceiros.FindAsync(id);
            if (terceiros == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", terceiros.IdUsuario);
            return View(terceiros);
        }

        // POST: Terceiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTerceiros,NomeTerceiros,TelefoneTerceiros,Telefone2Terceiros,EmailTerceiros,EnderecoTerceiros,CidadeTerceiros,BairroTerceiros,UFTerceiros,CEPTerceiros,ObservacaoTerceiros,DataCadastroTerceiros,DataUltimaMovimentacao,StatusTerceiros,IdUsuario")] Terceiros terceiros)
        {
            if (id != terceiros.IdTerceiros)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terceiros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerceirosExists((int)terceiros.IdTerceiros))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", terceiros.IdUsuario);
            return View(terceiros);
        }

        // GET: Terceiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Terceiros == null)
            {
                return NotFound();
            }

            var terceiros = await _context.Terceiros
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.IdTerceiros == id);
            if (terceiros == null)
            {
                return NotFound();
            }

            return View(terceiros);
        }

        // POST: Terceiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Terceiros == null)
            {
                return Problem("Entity set 'ProsperaContext.Terceiros'  is null.");
            }
            var terceiros = await _context.Terceiros.FindAsync(id);
            if (terceiros != null)
            {
                _context.Terceiros.Remove(terceiros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerceirosExists(int id)
        {
          return (_context.Terceiros?.Any(e => e.IdTerceiros == id)).GetValueOrDefault();
        }


    }
}
