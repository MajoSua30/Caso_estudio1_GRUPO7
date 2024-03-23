using System.ComponentModel.DataAnnotations;

namespace Caso_estudio1_GRUPO7.Models
{
    public class GameBoard
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public Usuarios? User { get; set; }

        public int Duration { get; set; }
        public string? Result { get; set; }

    }
}

