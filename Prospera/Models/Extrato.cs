using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Prospera.Models
{
    public class Extrato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdExtrato { get; set; }

        [StringLength(25)]
        public string? NomeExtrato { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorExtrato { get; set; }

        [StringLength(120)]
        public string? DestinatarioExtrato { get; set; }

        public int RemetenteExtrato { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataExtrato { get; set; }

        [StringLength(20)]
        public string? StatusExtrato { get; set; }

        [StringLength(80)]
        public string? ObservacaoExtrato { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
