using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prospera.Models
{
    public class Terceiros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdTerceiros { get; set; }

        [Required(ErrorMessage ="Campo obrigatório!")]
        [StringLength(120)]
        public string NomeTerceiros { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Campo obrigatório!")]

        public string TelefoneTerceiros { get; set; }

        [StringLength(20)]
        public string? Telefone2Terceiros { get; set; }

        [StringLength(120)]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [Required(ErrorMessage = "Campo obrigatório!")]

        public string EmailTerceiros { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string EnderecoTerceiros { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string CidadeTerceiros { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string BairroTerceiros { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string UFTerceiros { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string CEPTerceiros { get; set; }

        [StringLength(80)]
        public string? ObservacaoTerceiros { get; set; }

        [DataType(DataType.Date)]

        public DateTime DataCadastroTerceiros { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataUltimaMovimentacao { get; set; }

        [StringLength(20)]
        public string StatusTerceiros { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
