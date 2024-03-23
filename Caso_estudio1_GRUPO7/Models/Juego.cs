using System;
using System.Collections.Generic;
using System.Linq;

namespace Caso_estudio1_GRUPO7
{
    public class Juego
    {
        public char[,] Tablero { get; private set; }
        public bool JuegoTerminado { get; private set; }
        public bool Empate { get; private set; }
        public char JugadorActual { get; private set; }
        public List<Tuple<int, int>> Movimientos { get; private set; }

        public Juego()
        {
            Tablero = new char[3, 3];
            InicializarTablero();
            JugadorActual = 'X'; // El jugador comienza como X
            Movimientos = new List<Tuple<int, int>>();
        }

        private void InicializarTablero()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tablero[i, j] = ' ';
                }
            }
        }

        public void Jugar(int fila, int columna)
        {
            if (Tablero[fila, columna] != ' ')
            {
                throw new InvalidOperationException("La casilla seleccionada ya está ocupada.");
            }

            Tablero[fila, columna] = JugadorActual;
            Movimientos.Add(new Tuple<int, int>(fila, columna)); // Guardar el movimiento
            VerificarEstadoJuego();
            CambiarJugador();
        }


        public void JugadaComputadora()
        {
            Random random = new Random();
            int fila;
            int columna;

            do
            {
                fila = random.Next(0, 3);
                columna = random.Next(0, 3);
            } while (Tablero[fila, columna] != ' ');

            Tablero[fila, columna] = JugadorActual;
            Movimientos.Add(new Tuple<int, int>(fila, columna)); // Registrar el movimiento

            VerificarEstadoJuego();
            CambiarJugador();
        }


        private void VerificarEstadoJuego()
        {
            // Verificar filas
            for (int i = 0; i < 3; i++)
            {
                if (Tablero[i, 0] != ' ' && Tablero[i, 0] == Tablero[i, 1] && Tablero[i, 1] == Tablero[i, 2])
                {
                    JuegoTerminado = true;
                    return;
                }
            }

            // Verificar columnas
            for (int j = 0; j < 3; j++)
            {
                if (Tablero[0, j] != ' ' && Tablero[0, j] == Tablero[1, j] && Tablero[1, j] == Tablero[2, j])
                {
                    JuegoTerminado = true;
                    return;
                }
            }

            // Verificar diagonales
            if (Tablero[0, 0] != ' ' && Tablero[0, 0] == Tablero[1, 1] && Tablero[1, 1] == Tablero[2, 2])
            {
                JuegoTerminado = true;
                return;
            }

            if (Tablero[0, 2] != ' ' && Tablero[0, 2] == Tablero[1, 1] && Tablero[1, 1] == Tablero[2, 0])
            {
                JuegoTerminado = true;
                return;
            }

            // Verificar empate
            Empate = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Tablero[i, j] == ' ')
                    {
                        Empate = false;
                        return;
                    }
                }
            }
        }

        private void CambiarJugador()
        {
            JugadorActual = (JugadorActual == 'X') ? 'O' : 'X';
        }
    }
}
