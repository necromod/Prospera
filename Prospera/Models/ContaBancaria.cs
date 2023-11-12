using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prospera.Models
{
    public class ContaBancaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContaBancaria { get; set; }

        [StringLength(50)]
        public string? BancoContBan {  get; set; }

        [StringLength(120)]
        public string? TitularContBan { get; set; }

        public int?CodigoContaBanc { get; set; }

        [Obsolete]
        public int NumContBan { get; set; }

        public int AgenciaContBan { get; set; }

        [StringLength(50)]
        public string TipoContBan { get; set; }

        [DataType(DataType.Currency)]
        public decimal SaldoContBan { get; set; }

        [StringLength(80)]
        public string ObsContBan { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Terceiros")]
        public int IdTerceiros { get; set; }
        public virtual Terceiros Terceiros { get; set; }
    }
}
