namespace Caso_estudio1_GRUPO7.Models
{
    public class Juego
    {
        public char[,] Tablero { get; set; }
        public char TurnoActual { get; set; }

        public Juego()
        {
            Tablero = new char[3, 3];
            ReiniciarTablero();
        }

        public void ReiniciarTablero()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tablero[i, j] = '-';
                }
            }
            TurnoActual = 'X';
        }
    }
}