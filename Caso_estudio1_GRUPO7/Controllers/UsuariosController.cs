using Microsoft.AspNetCore.Mvc;
using Caso_estudio1_GRUPO7.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class UsuariosController : Controller
{
    private readonly CasoContext _context;

    public UsuariosController()
    {
        _context = new CasoContext();
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
            // Encriptar la contraseña antes de guardarla
            usuario.Password = HashPassword(usuario.Password);

            _context.Users.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        return View(usuario);
    }

    // GET: Usuarios/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Usuarios/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = HashPassword(login.Password);
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == hashedPassword);

            if (user != null)
            {
                // Usuario autenticado, realiza las acciones necesarias
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
        }

        return View(login);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}