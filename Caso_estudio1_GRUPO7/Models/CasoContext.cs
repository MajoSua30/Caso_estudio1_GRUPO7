using Microsoft.EntityFrameworkCore;

namespace Caso_estudio1_GRUPO7.Models
{
    public class CasoContext : DbContext
    {
        public CasoContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=Acel;Database=GatoImp;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Usuarios> Users { get; set; }
        public DbSet<GameBoard> GameBoards { get; set; }
    }
}
