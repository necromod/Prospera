using Prospera.Data;
using Prospera.Models;

namespace Prospera.Helpers
{
    public class TerceirosViewModelInterface : TerceirosViewModel
    {
        private readonly ProsperaContext _context;

        public TerceirosViewModelInterface(ProsperaContext context)
        {
            _context = context;
        }

        public void PreencherListaTerceiros()
        {
            ListaTerceiros = _context.Terceiros.ToList();
        }
    }
}
