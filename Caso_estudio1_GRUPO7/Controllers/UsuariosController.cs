using Microsoft.AspNetCore.Mvc;
using Caso_estudio1_GRUPO7.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http; // Necesario para IFormFile
using System.IO; // Necesario para MemoryStream

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

    // GET: Usuarios/Login
    public IActionResult Login()
    {
        return View();
    }


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

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RegisterWithPhoto(Usuarios usuario, IFormFile photoFile)
    {
        if (ModelState.IsValid)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    photoFile.CopyTo(memoryStream);
                    usuario.Photo = memoryStream.ToArray(); // Guarda la foto como un array de bytes
                }
            }

            // Encriptar la contraseña antes de guardarla
            usuario.Password = HashPassword(usuario.Password);

            _context.Users.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        return View("Register", usuario); 
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
              
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
        }

        return View(login);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}