using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;
using Microsoft.AspNetCore.Authorization;


namespace Prospera.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;


        public ContasController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        // GET: Contas
        public async Task<IActionResult> Index()
        {
            var prosperaContext = _context.Contas.Include(c => c.Usuario);
            return View(await prosperaContext.ToListAsync());
        }

        public IActionResult CreateReceitas()
        {
            return View();
        }
        public IActionResult CreateDespesas()
        {
            return View();
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario");
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContas,CodigoCont,TipoCont,DatEmissaoCont,DatVenciCont,RecebedorCont,PagadorCont,Descricaocont,ValorCont,StatusCont,MetodoPgtoCont,ObservacaoCont,IdUsuario")] Contas contas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contas.IdUsuario);
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contas.IdUsuario);
            return View(contas);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContas,CodigoCont,TipoCont,DatEmissaoCont,DatVenciCont,RecebedorCont,PagadorCont,Descricaocont,ValorCont,StatusCont,MetodoPgtoCont,ObservacaoCont,IdUsuario")] Contas contas)
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "IdUsuario", "CPFUsuario", contas.IdUsuario);
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

        [HttpPost]
        public IActionResult CadastrarDespesa(Contas contas, string btnAcao)
        {

            //Botão Cadastro apertado
            if (btnAcao == "Cadastro")
            {
                //Configuração de usuário a partir de claims ou sessão
                var userId = HttpContext.GetUserId() ?? _sessao.BuscarSessaoUsuario()?.IdUsuario ?? 55;
                contas.IdUsuario = userId;

                //Inserção automática dos campos
                int proximoCodigoCont = EncontrarProximoCodigoCont(contas.IdUsuario);
                contas.CodigoCont = proximoCodigoCont;
                contas.PagarReceberCont = "Despesa";
                contas.TipoCont = 1;
                contas.DatEmissaoCont = DateTime.Now;
                contas.PessoaCont = Convert.ToString(contas.IdUsuario);
                contas.PagadorCont = Convert.ToString(contas.IdUsuario);
                contas.Descricaocont = " ";

                //Criação do campo dentro do banco de dados
                _context.Contas.Add(contas);
                _context.SaveChanges();
            }
            //Botão Excluir apertado
            if (btnAcao == "Excluir")
            {
                if (int.TryParse(contas.CodigoCont.ToString(), out int id))
                {
                    //Carrega sessão de usuário
                    var usuarioLogado = _sessao.BuscarSessaoUsuario();
                    if (usuarioLogado == null)
                    {
                        return (null);
                    }
                    //Verifica se a conta existe
                    var DespesaExiste = _context.Contas
                        .FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.CodigoCont == contas.CodigoCont);

                    if (DespesaExiste != null)
                    {
                        // O ID existe no banco de dados, você pode excluí-lo.
                        _context.Contas.Remove(DespesaExiste);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return (null);
                    }

                }
                else
                {
                    Console.WriteLine("entrada do usuário não é um número inteiro válido");
                }

            }
            //Botão Alterar apertado
            if (btnAcao == "Alterar")
        {
            if (contas.CodigoCont > 0)
            {
                //Carrega sessão de usuário
                var usuarioLogado = _sessao.BuscarSessaoUsuario();
                if (usuarioLogado == null) return (null);
                //Verifica se a conta existe
                var DespesaExiste = _context.Contas
                    .FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.CodigoCont == contas.CodigoCont);
                if(DespesaExiste != null)
                {
                    // Atualize o registro existente com os novos valores
                    DespesaExiste.NomeCont = contas.NomeCont;
                    DespesaExiste.ObservacaoCont = contas.ObservacaoCont;
                    DespesaExiste.ValorCont = contas.ValorCont;
                    DespesaExiste.DatVenciCont = contas.DatVenciCont;
                    DespesaExiste.MetodoPgtoCont = contas.MetodoPgtoCont;
                    DespesaExiste.StatusCont = contas.StatusCont;
                    DespesaExiste.RecebedorCont = contas.RecebedorCont;
                    contas.DatEmissaoCont = DateTime.Now;

                    _context.SaveChanges();
                }
                else
                {
                    return (null);
                }


            }
            else
            {
                return (null);
            }


        }



            return RedirectToAction("ConsultaExtrato", "Extrato");
        }

        private int EncontrarProximoCodigoCont(int idUsuario)
        {
            // Verifique o maior CodigoCont para o usuário especificado
            int? maxCodigoCont = _context.Contas
                .Where(c => c.IdUsuario == idUsuario)
                .Max(c => (int?)c.CodigoCont);

            // Calcule o próximo CodigoCont com base no máximo encontrado
            int proximoCodigoCont = (maxCodigoCont ?? 0) + 1;

            return proximoCodigoCont;
        }



        [HttpGet]
        public IActionResult BuscarDespesas(int id)
        {
            Console.WriteLine($"O método BuscarContas foi chamado corretamente");  // Adicione esta linha para verificar no console

            // Obtenha o usuário logado (você pode usar sua lógica de sessão)
            Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();// Implemente sua lógica para obter o usuário logado

            if (usuarioLogado == null)
            {
                Console.WriteLine($"Usuário sem login");
                return Json(null); // Usuário não logado, tratamento apropriada aqui
            }

            // Realize a consulta na tabela de Contas
            var contaEncontrada = _context.Contas
                .FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.CodigoCont == id);
            return Json(contaEncontrada);
        }

        public IActionResult CadastrarReceita(Contas contas, string btnAcao)
        {

            //Botão Cadastro apertado
            if (btnAcao == "Cadastro")
            {
                //Configuração de sessão usuário
                if (_sessao.BuscarSessaoUsuario() != null)
                {
                    Usuario usuarioModel = _sessao.BuscarSessaoUsuario();
                    contas.IdUsuario = usuarioModel.IdUsuario;
                }
                else
                {
                    contas.IdUsuario = 55;
                }


                //Inserção automática dos campos
                // Encontre o próximo CodigoCont para o usuário atual
                int proximoCodigoCont = EncontrarProximoCodigoCont(contas.IdUsuario);
                contas.CodigoCont = proximoCodigoCont;
                contas.TipoCont = 2;
                contas.PagarReceberCont = "Receita";
                contas.DatEmissaoCont = DateTime.Now;
                contas.PessoaCont = Convert.ToString(contas.IdUsuario);
                contas.PagadorCont = Convert.ToString(contas.IdUsuario);
                contas.Descricaocont = " ";

                //Criação do campo dentro do banco de dados
                _context.Contas.Add(contas);
                _context.SaveChanges();
            }
            //Botão Excluir apertado
            if (btnAcao == "Excluir")
            {
                if (int.TryParse(contas.CodigoCont.ToString(), out int id))
                {
                    //Carrega sessão de usuário
                    Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();
                    //Verifica se a conta existe
                    var DespesaExiste = _context.Contas
                        .FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.CodigoCont == contas.CodigoCont);

                    if (DespesaExiste != null)
                    {
                        // O ID existe no banco de dados, você pode excluí-lo.
                        _context.Contas.Remove(DespesaExiste);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return (null);
                    }

                }
                else
                {
                    Console.WriteLine("entrada do usuário não é um número inteiro válido");
                }

            }
            //Botão Alterar apertado
            if (btnAcao == "Alterar")
            {
                if (contas.CodigoCont > 0)
                {
                    //Carrega sessão de usuário
                    Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();
                    //Verifica se a conta existe
                    var DespesaExiste = _context.Contas
                        .FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.CodigoCont == contas.CodigoCont);
                    if (DespesaExiste != null)
                    {
                        // Atualize o registro existente com os novos valores
                        DespesaExiste.NomeCont = contas.NomeCont;
                        DespesaExiste.ObservacaoCont = contas.ObservacaoCont;
                        DespesaExiste.ValorCont = contas.ValorCont;
                        DespesaExiste.DatVenciCont = contas.DatVenciCont;
                        DespesaExiste.MetodoPgtoCont = contas.MetodoPgtoCont;
                        DespesaExiste.StatusCont = contas.StatusCont;
                        DespesaExiste.RecebedorCont = contas.RecebedorCont;
                        contas.DatEmissaoCont = DateTime.Now;

                        _context.SaveChanges();
                    }
                    else
                    {
                        return (null);
                    }


                }
                else
                {
                    return (null);
                }


            }



            return RedirectToAction("ConsultaExtrato", "Extrato");
        }

    }
}
