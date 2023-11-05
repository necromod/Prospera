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

        public MenuUsuarioController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }
        public async Task<IActionResult> MenuUsuario()
        {
            var prosperaContext = _context.Terceiros.Include(t => t.Usuario);
            return View("~/Views/Home/MenuUsuario.cshtml", await prosperaContext.ToListAsync());
        }


        



    }
}
