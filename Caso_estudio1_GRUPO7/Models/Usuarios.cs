using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caso_estudio1_GRUPO7.Models
{
    public class Usuarios
    {
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[]? Photo { get; set; }
    }
}
