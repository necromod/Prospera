using System.Collections.Generic;
using Prospera.Models;
using Microsoft.AspNetCore.Mvc;
using Prospera.Data;
using Prospera.Controllers;
using Prospera.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Prospera.Helpers
{
    public class TerceirosViewModel
    {
        public IEnumerable<Terceiros> ListaTerceiros { get; set; } = new List<Terceiros>();
        public Terceiros NovoTerceiro { get; set; } = new Terceiros();

        /*public void PreencherTerceiro(Terceiros terceiroModel)
        {
            Terceiros usuarioCadastrar = terceiroModel;            

            if (usuarioCadastrar != null) {
                Console.WriteLine("Dados do terceiro não chegou no TerceirosInterface");
            }
            else
            {
                NovoTerceiro = usuarioCadastrar;
            }
        }*/
    }
}
