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

        [Column(TypeName = "VARCHAR(MAX)")]
        public string Photo { get; set; }
    }
}
