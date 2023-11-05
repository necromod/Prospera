using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class MenuUsuarioController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;
        private readonly TerceirosViewModel _teerceirosViewModel;

        public MenuUsuarioController(ProsperaContext context, SessaoInterface sessao, TerceirosViewModel teerceirosViewModel)
        {
            _context = context;
            _sessao = sessao;
            _teerceirosViewModel = teerceirosViewModel; 
        }

        public IActionResult MenuUsuario()
        {
            var viewModel = new TerceirosViewModelInterface(_context);
            viewModel.PreencherListaTerceiros();

            // Agora, viewModel.ListaTerceiros está populado com os dados da tabela Terceiros

            return View("~/Views/Home/MenuUsuario.cshtml", viewModel);
        }



        // POST: Criação de campo Terceiros
        [HttpPost]
        public IActionResult CadastrarTerceiro(TerceirosViewModel terceiros)
        {

            //Terceiros testeExterno = new Terceiros();
            //testeExterno = _teerceirosViewModel.NovoTerceiro();

            //Verifica se o usuário está logado
            if (_sessao.BuscarSessaoUsuario() != null)
            {
                Usuario usuarioModel = _sessao.BuscarSessaoUsuario();
                terceiros.NovoTerceiro.IdUsuario = usuarioModel.IdUsuario;
            }
            else
            {
                terceiros.NovoTerceiro.IdUsuario = 55;
            }

            

            //inserção automática dos campos
            terceiros.NovoTerceiro.DataCadastroTerceiros = DateTime.Now;
            terceiros.NovoTerceiro.DataUltimaMovimentacao = DateTime.Now;
            terceiros.NovoTerceiro.StatusTerceiros = "Ativo";

            //Criação do campo dentro do banco de dados
            _context.Terceiros.Add(terceiros.NovoTerceiro);
            _context.SaveChanges();

            return RedirectToAction("MenuUsuario", "MenuUsuario");
        }
    }
}
