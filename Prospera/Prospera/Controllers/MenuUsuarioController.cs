using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prospera.Data;
using Prospera.Models;
using Prospera.Helpers;

namespace Prospera.Controllers
{
    public class MenuUsuarioController : Controller
    {

        private readonly SessaoInterface _sessao;
        private readonly ProsperaContext _context;

        public MenuUsuarioController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }


        //Método de criação de Receitas (Tabela Contas -> TipoCont:Depósito)
        [HttpPost]
        public IActionResult CadastrarReceita(Contas contasModel)
        {
            //Iserção auomática de campos
            //Inserir: CodigoCont
            //usuario.DatUltimoAcesUsuario = DateTime.Now;
            contasModel.DatEmissaoCont = DateTime.Now;
            _context.Contas.Add(contasModel);
            _context.SaveChanges();
            return View(contasModel);

        }
    }
}
