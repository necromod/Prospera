﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prospera.Models
{
    public class Contas // Contas serão tabelas para armazenar contas gerais, água, luz, ou até mesmo boletos a pagar.
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContas { get; set; }

        public int? CodigoCont { get; set; } //Código da conta/boleto pra referência

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeCont { get; set; }// Nome da conta a ser exibida

        public int? TipoCont { get; set; } //Campo não mais utilizado
        public string? PagarReceberCont { get; set; } //Definição se é uma conta para Pagar ou Para receber

        [DataType(DataType.Date)]
        public DateTime? DatEmissaoCont { get; set; } // Data de emissão da conta

        [DataType(DataType.Date)]
        public DateTime? DatVenciCont { get; set; } // Data de vencimento da conta

        public string? PessoaCont { get; set; } // Redundância para instâncias em outras tabelas

        [StringLength(120)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? RecebedorCont { get; set; } // Quem estará recebendo o valor da conta

        [StringLength(120)]
        public string? PagadorCont { get; set; } // Quem estará pagando o valor da conta

        [StringLength(80)]
        public string? Descricaocont { get; set; } // Descrição da conta, como "Conta de água"

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public decimal ValorCont { get; set; } // Valor da conta a ser paga

        [StringLength(20)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string StatusCont { get; set; } // Descrição se conta já está paga

        [StringLength(20)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string MetodoPgtoCont { get; set; } // Cartão, Boleto, Pix, etc..

        [StringLength(80)]
        public string? ObservacaoCont { get; set; } // Observações adicionais

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
