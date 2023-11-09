using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prospera.Models
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(120)]
        public string NomeUsuario { get; set; }

        [Required]
        [StringLength(120)]
        public string EmailUsuario { get; set; }

        [Required]
        [StringLength(20)]
        public string SenhaUsuario { get; set; }

        [Required]
        [StringLength(20)]
        public string CPFUsuario { get; set; }

        [StringLength(80)]
        public string CargoUsuario { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatCadastroUsuario { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatUltimoAcesUsuario { get; set; }

        [StringLength(20)]
        public string StatusUsuario { get; set; }

    }
}
