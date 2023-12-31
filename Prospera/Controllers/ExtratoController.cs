﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class ExtratoController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;


        public ExtratoController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;   
        }

        // GET: Extrato

        public IActionResult ConsultaExtrato()
        {
            var usuarioLogado = _sessao.BuscarSessaoUsuario();
            //Verifica Sessão de usuário
            if (usuarioLogado == null)
            {
                usuarioLogado = _context.Usuario.FirstOrDefault(t => t.IdUsuario == 1);
                _sessao.CriarSessaoUsuario(usuarioLogado);
                Console.WriteLine("Usuário logado: ", usuarioLogado.NomeUsuario);
            }
            else
            {
                Console.WriteLine("Usuário logado: ", usuarioLogado.NomeUsuario);
            }
            var contasDoUsuario = _context.Contas
            .Where(c => c.IdUsuario == usuarioLogado.IdUsuario)
            .ToList();


            return View(contasDoUsuario);
        }

    }
}
