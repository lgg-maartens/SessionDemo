using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SessionDemo.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace SessionDemo.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      ViewData["user"] = HttpContext.Session.GetString("User");

      return View();
    }

    public IActionResult Login(string username, string password)
    {
      if (password == "geheim")
      {
        HttpContext.Session.SetString("User", username);
        return Redirect("/");
      }

      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    static string ComputeSha256Hash(string rawData)
    {
      // Create a SHA256   
      using (SHA256 sha256Hash = SHA256.Create())
      {
        // ComputeHash - returns byte array  
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

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
}
