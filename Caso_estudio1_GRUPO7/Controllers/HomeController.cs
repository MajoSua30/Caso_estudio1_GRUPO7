using Microsoft.AspNetCore.Mvc;
using Caso_estudio1_GRUPO7.Models;

namespace Caso_estudio1_GRUPO7.Controllers
{
    public class HomeController : Controller
    {
        private Juego juego; // Mantenemos una instancia del juego en el controlador

        public HomeController()
        {
            juego = new Juego();
        }

        public IActionResult Index()
        {
            return View(juego);
        }

        [HttpPost]
        public IActionResult Jugada(int fila, int columna)
        {
            if (juego.Tablero[fila, columna] != ' ')
            {
                ViewBag.Message = "La casilla seleccionada ya está ocupada. Por favor, elige otra.";
            }
            else
            {
                juego.Jugar(fila, columna);
                if (!juego.JuegoTerminado && !juego.Empate)
                {
                    juego.JugadaComputadora();
                }
            }
            return View("Index", juego);
        }
    }
}