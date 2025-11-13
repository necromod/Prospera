using Prospera.Data;
using Prospera.Models;

namespace Prospera.Helpers
{
    public class TerceirosViewModelInterface : TerceirosViewModel
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;

        public TerceirosViewModelInterface(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        public void PreencherListaTerceiros()
        {
            var usuarioLogado = _sessao.BuscarSessaoUsuario();
            //Verifica Sessão de usuário
            if (usuarioLogado == null)
            {
                usuarioLogado = _context.Usuario?.FirstOrDefault(t => t.IdUsuario == 1);
                if (usuarioLogado != null)
                {
                    _sessao.CriarSessaoUsuario(usuarioLogado);
                    Console.WriteLine("Usuário logado: " + usuarioLogado.NomeUsuario);
                }
                else
                {
                    ListaTerceiros = new List<Terceiros>();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Usuário logado: " + usuarioLogado.NomeUsuario);
            }

            ListaTerceiros = _context.Terceiros?.Where(t => t.IdUsuario ==  usuarioLogado.IdUsuario).ToList() ?? new List<Terceiros>();
        }
    }
}
