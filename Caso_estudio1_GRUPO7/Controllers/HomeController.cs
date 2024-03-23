using Microsoft.AspNetCore.Mvc;
using System;

namespace Caso_estudio1_GRUPO7.Controllers
{
    public class HomeController : Controller
    {
        private static char[,] _tablero = new char[3, 3];
        private static char _turnoActual = 'X';

        public IActionResult Index()
        {
            ViewBag.TurnoActual = _turnoActual;
            ViewBag.Resultado = TempData["Resultado"]?.ToString();
            return View("Index", _tablero);
        }

        [HttpPost]
        public IActionResult RealizarMovimiento(int fila, int columna)
        {
            // Si la casilla ya está ocupada, simplemente redirige a Index
            if (_tablero[fila, columna] != '-')
            {
                TempData["Resultado"] = "Movimiento no válido, casilla ya ocupada.";
                return RedirectToAction("Index");
            }

            // Realiza el movimiento del jugador
            _tablero[fila, columna] = _turnoActual;

            // Verifica si el movimiento del jugador conduce a una victoria o empate
            if (VerificarFinDelJuego(_turnoActual))
            {
                return RedirectToAction("Index");
            }

            // Cambia el turno al oponente
            _turnoActual = _turnoActual == 'X' ? 'O' : 'X';

            // Realiza el movimiento de la computadora automáticamente
            RealizarMovimientoComputadora();

            // Verifica si el movimiento de la computadora conduce a una victoria o empate
            VerificarFinDelJuego(_turnoActual);

            // Cambia el turno de nuevo al jugador
            _turnoActual = 'X';

            return RedirectToAction("Index");
        }

        private void RealizarMovimientoComputadora()
        {
            // Lógica simple para elegir una casilla vacía al azar
            Random random = new Random();
            bool movimientoHecho = false;
            while (!movimientoHecho)
            {
                int fila = random.Next(3);
                int columna = random.Next(3);
                if (_tablero[fila, columna] == '-')
                {
                    _tablero[fila, columna] = _turnoActual;
                    movimientoHecho = true;
                }
            }
        }

        private bool VerificarFinDelJuego(char jugador)
        {
            if (HayGanador(jugador))
            {
                TempData["Resultado"] = jugador == 'X' ? "¡X ha ganado!" : "¡O ha ganado!";
                ReiniciarJuego();
                return true;
            }
            else if (Empate())
            {
                TempData["Resultado"] = "¡Empate!";
                ReiniciarJuego();
                return true;
            }
            return false;
        }

        // Métodos HayGanador y Empate aquí...

        private void ReiniciarJuego()
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