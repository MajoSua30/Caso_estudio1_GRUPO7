using Microsoft.AspNetCore.Mvc;
using System;

namespace Caso_estudio1_GRUPO7.Controllers
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
            ViewBag.TurnoActual = _turnoActual;
            return View(_tablero);
        }

        [HttpPost]
        public IActionResult RealizarMovimiento(int fila, int columna)
        {
            if (_tablero[fila, columna] == '-')
            {
                _tablero[fila, columna] = _turnoActual;
                if (HayGanador(_turnoActual))
                {
                    ViewBag.Resultado = _turnoActual == 'X' ? "¡Has ganado!" : "¡Has perdido!";
                    ReiniciarJuego();
                    return View("Index", _tablero);
                }
                else if (Empate())
                {
                    ViewBag.Resultado = "¡Empate!";
                    ReiniciarJuego();
                    return View("Index", _tablero);
                }

                // Cambiar turno
                _turnoActual = _turnoActual == 'X' ? 'O' : 'X';

                if (_turnoActual == 'O')
                {
                    RealizarMovimientoComputadora();
                    if (HayGanador(_turnoActual))
                    {
                        ViewBag.Resultado = "¡Has perdido!";
                        ReiniciarJuego();
                        return View("Index", _tablero);
                    }
                    else if (Empate())
                    {
                        ViewBag.Resultado = "¡Empate!";
                        ReiniciarJuego();
                        return View("Index", _tablero);
                    }
                    _turnoActual = 'X'; // Volver al turno del jugador
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        private void RealizarMovimientoComputadora()
        {
            Random random = new Random();
            int fila, columna;
            do
            {
                fila = random.Next(3);
                columna = random.Next(3);
            } while (_tablero[fila, columna] != '-');

            _tablero[fila, columna] = _turnoActual;
        }

        private bool HayGanador(char jugador)
        {
            // Verificar filas, columnas y diagonales
            for (int i = 0; i < 3; i++)
            {
                if ((_tablero[i, 0] == jugador && _tablero[i, 1] == jugador && _tablero[i, 2] == jugador) ||
                    (_tablero[0, i] == jugador && _tablero[1, i] == jugador && _tablero[2, i] == jugador))
                    return true;
            }

            // Diagonales
            if ((_tablero[0, 0] == jugador && _tablero[1, 1] == jugador && _tablero[2, 2] == jugador) ||
                (_tablero[0, 2] == jugador && _tablero[1, 1] == jugador && _tablero[2, 0] == jugador))
                return true;

            return false;
        }

        private bool Empate()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_tablero[i, j] == '-')
                        return false;
                }
            }
            return true;
        }

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