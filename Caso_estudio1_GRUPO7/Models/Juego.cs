namespace Caso_estudio1_GRUPO7.Models
{
    public class Juego
    {
        public char[,] _tablero { get; set; }
        public char _turnoActual { get; set; }

        public Juego()
        {
            _tablero = new char[3, 3];
            ReiniciarTablero();
        }

        public void ReiniciarTablero()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _tablero[i, j] = '-';
                }
            }
            _turnoActual = 'X';
        }
    }
}