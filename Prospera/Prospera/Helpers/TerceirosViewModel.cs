using System.Collections.Generic;
using Prospera.Models;

namespace Prospera.Helpers
{
    public class TerceirosViewModel
    {
        public IEnumerable<Terceiros> ListaTerceiros { get; set; }
        public Terceiros NovoTerceiro { get; set; }
    }
}
