using Microsoft.AspNetCore.Mvc;
using Caso_estudio1_GRUPO7.Models;

public class UsuariosController : Controller
{
    private readonly CasoContext _context;

    public UsuariosController(CasoContext context)
    {
        _context = context;
    }

    // GET: Usuarios/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Usuarios/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Usuarios usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home"); // Redirige a la vista principal después del registro exitoso
        }

        return View(usuario);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user != null)
            {
                // Usuario autenticado, realiza las acciones necesarias (por ejemplo, establecer la sesión)
                // Puedes redirigir a la vista principal o a cualquier otra vista que desees
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
        }

        return View(login);
    }

}
