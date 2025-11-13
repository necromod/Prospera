using Prospera.Models;

namespace Prospera.Helpers
{
    public interface SessaoInterface
    {
        void CriarSessaoUsuario(Usuario usuariomodel);
        void RemoverSessaoUsuario();
        Usuario? BuscarSessaoUsuario();
    }
}
