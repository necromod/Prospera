using System.ComponentModel.DataAnnotations;

namespace Prospera.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo de email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "O campo de senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string SenhaUsuario { get; set; }
    }

}
