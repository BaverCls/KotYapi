using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KOT_YAPI.Models;
using Proje.Models;
using Proje.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace KOT_YAPI.Controllers;

public class HomeController : Controller
{

    
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult References()
    {
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }

    public IActionResult Contact()
    {
        if (TempData["Message"] != null)
        {
            ViewBag.Message = TempData["Message"];
        }
        return View();
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Contact(ContactMessage model)
    {
        if (ModelState.IsValid)
        {
            _context.ContactMessages.Add(model);
            _context.SaveChanges();
            TempData["Message"] = "Mesajınız başarıyla gönderildi!";
            return RedirectToAction("Contact");
        }
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize(Roles = "Admin")]
    public IActionResult ContactList()
    {
        var messages = _context.ContactMessages.ToList();
        return View(messages);
    }
    public IActionResult DeleteContact(int id)
    {
        var message = _context.ContactMessages.FirstOrDefault(x => x.Id == id);
        if (message != null)
        {
            _context.ContactMessages.Remove(message);
            _context.SaveChanges();
        }
        return RedirectToAction("ContactList");
    }
    public async Task<IActionResult> Login(string username, string password)
    {
        // Database de kullanabilirdim de ama bu kadar basit bir uygulama için gerek yok
    if (username == "baver" && password == "baver123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Kullanıcı adı veya şifre yanlış!";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
