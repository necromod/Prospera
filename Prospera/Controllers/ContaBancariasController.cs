using System;
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
    public class ContaBancariasController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;

        public ContaBancariasController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }
        public IActionResult CreateContasBancarias()
        {
            return View();
        }
        public IActionResult ConsultaContasBancarias()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarContBan(ContaBancaria contBan, string btnAcao)
        {
            //Botão Cadastro apertado
            if (btnAcao == "Cadastro")
            {
                //Configuração de sessão usuário
                var usuarioSess = _sessao.BuscarSessaoUsuario();
                if (usuarioSess != null)
                {
                    contBan.IdUsuario = usuarioSess.IdUsuario;
                    contBan.TitularContBan = usuarioSess.NomeUsuario;
                }
                else
                {
                    contBan.IdUsuario = 55;
                    contBan.TitularContBan = "Teste";
                }


                //Inserção automática dos campos
                // Encontre o próximo CodigoCont para o usuário atual
                //Criação do campo dentro do banco de dados
                contBan.CodigoContaBanc = 1;

                _context.ContaBancaria.Add(contBan);
                _context.SaveChanges();
            }
            //Botão Excluir apertado
            if (btnAcao == "Excluir")
            {
                if (int.TryParse(contBan.NumContBan.ToString(), out int id))
                {
                    //Carrega sessão de usuário
                    var usuarioLogado = _sessao.BuscarSessaoUsuario();
                    if (usuarioLogado == null)
                    {
                        return RedirectToAction("Login", "Usuario");
                    }
                    //Verifica se a conta existe
                    var ContBanExiste = _context.ContaBancaria.FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.NumContBan == contBan.NumContBan);

                    if (ContBanExiste != null)
                    {
                        // O ID existe no banco de dados, você pode excluí-lo.
                        _context.ContaBancaria.Remove(ContBanExiste);
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
                if (contBan.NumContBan > 0)
                {
                    //Carrega sessão de usuário
                    var usuarioLogado = _sessao.BuscarSessaoUsuario();
                    if (usuarioLogado == null)
                    {
                        return RedirectToAction("Login", "Usuario");
                    }
                    //Verifica se a conta existe
                    var ContBanExiste = _context.ContaBancaria.FirstOrDefault(c => c.IdUsuario == usuarioLogado.IdUsuario && c.NumContBan == contBan.NumContBan);
                    if (ContBanExiste != null)
                    {
                        // Atualize o registro existente com os novos valores
                        ContBanExiste.BancoContBan = contBan.BancoContBan;
                        ContBanExiste.TitularContBan = contBan.TitularContBan;
                        ContBanExiste.CodigoContaBanc = contBan.CodigoContaBanc;
                        ContBanExiste.NumContBan = contBan.NumContBan;
                        ContBanExiste.AgenciaContBan = contBan.AgenciaContBan;
                        ContBanExiste.TipoContBan = contBan.TipoContBan;
                        ContBanExiste.SaldoContBan = contBan.SaldoContBan;
                        ContBanExiste.ObsContBan = contBan.ObsContBan;

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



            return RedirectToAction("CreateContasBancarias", "ContaBancarias");
        }

         


    }
}
