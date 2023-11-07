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

        // POST: Criação de campo Terceiros
        [HttpPost]
        public IActionResult CadastrarTerceiro(Terceiros terceiros, string btnAcao)
        {
            //botão de Cadastrar foi pressionado
            if (btnAcao == "cadastro")
            {

                //Terceiros testeExterno = new Terceiros();
                //testeExterno = _teerceirosViewModel.NovoTerceiro();

                //Verifica se o usuário está logado
                if (_sessao.BuscarSessaoUsuario() != null)
                {
                    Usuario usuarioModel = _sessao.BuscarSessaoUsuario();
                    terceiros.IdUsuario = usuarioModel.IdUsuario;
                }
                else
                {
                    terceiros.IdUsuario = 55;
                }



                //inserção automática dos campos
                terceiros.IdTerceiros = 0;
                terceiros.DataCadastroTerceiros = DateTime.Now;
                terceiros.DataUltimaMovimentacao = DateTime.Now;
                terceiros.StatusTerceiros = "Ativo";

                //Criação do campo dentro do banco de dados
                _context.Terceiros.Add(terceiros);
                _context.SaveChanges();
            }

            //botão de exclusao foi pressionado
            else if (btnAcao == "exclusao")
            {
                // Certifique-se de que o ID seja uma string válida.
                if (int.TryParse(terceiros.IdTerceiros.ToString(), out int id))
                {
                    // Verifique se o ID existe no banco de dados.
                    var terceiro = _context.Terceiros.FirstOrDefault(t => t.IdTerceiros == id);

                    if (terceiro != null)
                    {
                        // O ID existe no banco de dados, você pode excluí-lo.
                        _context.Terceiros.Remove(terceiro);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Não funcionou IF terceiro != null");
                    }
                }
                else
                {
                    Console.WriteLine("entrada do usuário não é um número inteiro válido");
                    // A entrada do usuário não é um número inteiro válido, adote a ação apropriada.
                    // Você pode retornar uma mensagem de erro ou redirecionar para uma página apropriada.
                }
            }

            //botão de Alterar foi pressionado
            else if (btnAcao == "Alterar")
                {
                    if (int.TryParse(terceiros.IdTerceiros.ToString(), out int id))
                    {
                        var terceiro = _context.Terceiros.FirstOrDefault(t => t.IdTerceiros == id);

                        if (terceiro != null)
                        {
                            return RedirectToAction("Consulta", "Terceiros");
                        }
                    }
                }

            return RedirectToAction("Consulta", "Terceiros");

        }

        [HttpGet]
        public IActionResult BuscarTerceiros(int id)
        {
            var terceiros = _context.Terceiros.FirstOrDefault(t => t.IdTerceiros == id);

            if (terceiros != null)
            {
                return Json(terceiros); // Retorna o Terceiros encontrado como JSON.
            }
            else
            {
                return Json(null); // Retorna nulo se o Terceiros não for encontrado.
            }
        }



    }
}
