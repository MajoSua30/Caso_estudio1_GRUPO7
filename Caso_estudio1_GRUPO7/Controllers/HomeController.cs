using Microsoft.AspNetCore.Mvc;
using System;

namespace Grupo.Controllers
{
    public class HomeController : Controller
    {
        private char[,] _tablero;
        private char _turnoActual;

        public HomeController()
        {
            _tablero = new char[3, 3];
            ReiniciarJuego();
        }

        public IActionResult Index()
        {
            return View(_tablero);
        }

        [HttpPost]
        public IActionResult RealizarMovimiento(int fila, int columna)
        {
            // Verificar si la casilla está vacía
            if (_tablero[fila, columna] == '-')
            {
                // Marcar la casilla 
                _tablero[fila, columna] = 'X';

                // hay un ganador
                if (HayGanador('X'))
                {
                    ViewBag.Resultado = "¡Has ganado!";
                    return View("Resultado", _tablero);
                }

                // hay un empate
                if (Empate())
                {
                    ViewBag.Resultado = "¡Empate!";
                    return View("Resultado", _tablero);
                }

                //  turno de la computadora
                _turnoActual = 'O';

                //  movimiento de la computadora
                RealizarMovimientoComputadora();

                // Verificar si hay un ganador 
                if (HayGanador('O'))
                {
                    ViewBag.Resultado = "¡Has perdido!";
                    return View("Resultado", _tablero);
                }

                // Redireccionar de nuevo 
                return RedirectToAction("Index");
            }
            else
            {
                // Casilla ocupada
                return RedirectToAction("Error");
            }
        }

        private void RealizarMovimientoComputadora()
        {
            //  computadora realiza su movimiento
            Random random = new Random();
            int fila, columna;
            do
            {
                fila = random.Next(3);
                columna = random.Next(3);
            } while (_tablero[fila, columna] != '-');

            // Marcar la casilla con el símbolo de la computadora
            _tablero[fila, columna] = 'O';
        }

        private bool HayGanador(char jugador)
        {
      
            return false;
        }

        private bool Empate()
        {
           
            return false;
        }

        private void ReiniciarJuego()
        {
            // casillas vacías
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _tablero[i, j] = '-';
                }
            }

            //  empieza primero
            _turnoActual = 'X';
        }
    }
}
