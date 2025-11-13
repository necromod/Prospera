using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;
using Microsoft.AspNetCore.Authorization;

namespace Prospera.Controllers
{
    [Authorize]
    public class MenuUsuarioController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;
        private readonly TerceirosViewModel _teerceirosViewModel;
        private readonly ILogger<MenuUsuarioController> _logger;

        public MenuUsuarioController(ProsperaContext context, SessaoInterface sessao, TerceirosViewModel teerceirosViewModel, ILogger<MenuUsuarioController> logger)
        {
            _context = context;
            _sessao = sessao;
            _teerceirosViewModel = teerceirosViewModel;
            _logger = logger;
        }     

        // POST: Criação de campo Terceiros
        [HttpPost]
        public IActionResult CadastrarTerceiro(Terceiros terceiros, string btnAcao)
        {
            //botão de Cadastrar foi pressionado
            if (btnAcao == "cadastro")
            {
                //Verifica se o usuário está logado
                VerificaSessao(terceiros);


                //inserção automática dos campos
                //Encontre o próximo Codico
                int proximoCodigoTerceiro = ProximoTerceiroUsuario(terceiros.IdUsuario);
                terceiros.CodigoCont = proximoCodigoTerceiro;
                terceiros.IdTerceiros = 0;
                terceiros.DataCadastroTerceiros = DateTime.Now;
                terceiros.DataUltimaMovimentacao = DateTime.Now;
                terceiros.StatusTerceiros = "Ativo";
                terceiros.IdTerceiros = null;

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
                    var usuarioLogin = _sessao.BuscarSessaoUsuario();
                    if (usuarioLogin == null)
                    {
                        return RedirectToAction("Login", "Usuario");
                    }

                    // Verifique se o ID existe no banco de dados.
                    var terceiro = _context.Terceiros?.SingleOrDefault(t => t.IdUsuario == usuarioLogin.IdUsuario && t.CodigoCont == id);

                    if (terceiro != null)
                    {
                        // O ID existe no banco de dados, você pode excluí-lo.
                        _context.Terceiros.Remove(terceiro);
                        _context.SaveChanges();
                        _logger.LogInformation("Terceiro {Id} removido pelo usuário {UserId}", id, usuarioLogin.IdUsuario);
                    }
                    else
                    {
                        _logger.LogWarning("Tentativa de exclusão de terceiro inexistente. ID: {Id}, Usuário: {UserId}", id, usuarioLogin.IdUsuario);
                    }
                }
                else
                {
                    _logger.LogWarning("ID de terceiro inválido fornecido para exclusão");
                }
            }

            //botão de Alterar foi pressionado
            else if (btnAcao == "editar")
            {
                if (terceiros.IdTerceiros > 0)
                {
                    var usuarioLogin = _sessao.BuscarSessaoUsuario();
                    if (usuarioLogin == null)
                    {
                        return RedirectToAction("Login", "Usuario");
                    }

                    // Verifique se o ID existe no banco de dados.
                    var terceiroExistente = _context.Terceiros?.SingleOrDefault(t => t.IdUsuario == usuarioLogin.IdUsuario && t.CodigoCont == terceiros.IdTerceiros);

                    if (terceiroExistente != null)
                    {
                        // Atualize o registro existente com os novos valores
                        terceiroExistente.NomeTerceiros = terceiros.NomeTerceiros;
                        terceiroExistente.EmailTerceiros = terceiros.EmailTerceiros;
                        // Atualize outros campos da mesma forma

                        // Defina a data da última movimentação
                        terceiroExistente.DataUltimaMovimentacao = DateTime.Now;

                        // Salve as alterações no banco de dados
                        _context.SaveChanges();
                        _logger.LogInformation("Terceiro {Id} atualizado pelo usuário {UserId}", terceiros.IdTerceiros, usuarioLogin.IdUsuario);

                        return RedirectToAction("Consulta", "Terceiros");
                    }
                    else
                    {
                        _logger.LogWarning("Tentativa de edição de terceiro inexistente. ID: {Id}, Usuário: {UserId}", terceiros.IdTerceiros, usuarioLogin.IdUsuario);
                    }
                }
                else
                {
                    _logger.LogWarning("ID de terceiro inválido fornecido para edição");
                }
            }

            return RedirectToAction("Consulta", "Terceiros");

        }

        private void VerificaSessao(Terceiros terceiros)
        {
            var userId = HttpContext.GetUserId();
            if (userId.HasValue)
            {
                terceiros.IdUsuario = userId.Value;
            }
            else
            {
                var usuarioFromSessao = _sessao.BuscarSessaoUsuario();
                if (usuarioFromSessao != null)
                {
                    terceiros.IdUsuario = usuarioFromSessao.IdUsuario;
                }
                else
                {
                    terceiros.IdUsuario = 109;
                }
            }
        }

        private int ProximoTerceiroUsuario(int idUsuario)
        {
            // Obter todos os CodigoCont para o idUsuario ordenados de forma ascendente
            var codigosCont = _context.Terceiros?.Where(c => c.IdUsuario == idUsuario && c.CodigoCont.HasValue)
                                    .Select(c => c.CodigoCont.Value)
                                    .OrderBy(c => c)
                                    .ToList() ?? new List<int>();

            // Iterar pelos valores de CodigoCont para encontrar a primeira lacuna na sequência
            int proximoCodigoCont = 1;
            foreach (var codigo in codigosCont)
            {
                if (codigo == proximoCodigoCont)
                {
                    proximoCodigoCont++; // Se o valor atual for o esperado, avance para o próximo número
                }
                else
                {
                    break; // Se houver uma lacuna, saia do loop
                }
            }

            return proximoCodigoCont;
        }


        [HttpGet]
        public IActionResult BuscarTerceiros(int id)
        {
            var userId = HttpContext.GetUserId();
            if (!userId.HasValue)
            {
                return Json(null);
            }

            var terceiros = _context.Terceiros?.FirstOrDefault(t => t.CodigoCont == id && t.IdUsuario == userId.Value);

            if (terceiros != null)
            {
                return Json(terceiros);
            }
            else
            {
                return Json(null);
            }
        }


    }
}
