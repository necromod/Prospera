using Prospera.Data;
using Prospera.Models;
using System.Linq;

namespace Prospera.Helpers
{
    public class UserProvider : IUserProvider
    {
        private readonly ProsperaContext _context;
        public UserProvider(ProsperaContext context)
        {
            _context = context;
        }

        public Usuario? GetUserById(int id)
        {
            try
            {
                return _context.Usuario?.FirstOrDefault(u => u.IdUsuario == id);
            }
            catch
            {
                // If DB is not available or table missing, swallow exception and return null
                return null;
            }
        }
    }
}
