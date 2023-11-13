using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmProspera
{
    public class Usuario
    {

        public int IdUsuario { get; set; }


        public string NomeUsuario { get; set; }


        public string EmailUsuario { get; set; }


        public string SenhaUsuario { get; set; }


        public string CPFUsuario { get; set; }

        public string CargoUsuario { get; set; }

        public string DatCadastroUsuario { get; set; }

        public DateTime DatUltimoAcesUsuario { get; set; }
        public string StatusUsuario { get; set; }
        public string TpUsuario { get; set; }


    }
}
