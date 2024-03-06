using System.ComponentModel.DataAnnotations;

namespace Caso_estudio1_GRUPO7.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
