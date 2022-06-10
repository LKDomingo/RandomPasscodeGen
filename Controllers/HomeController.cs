using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscodeGen.Models;
using Microsoft.AspNetCore.Http;



namespace RandomPasscodeGen.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        if (HttpContext.Session.GetString("Passcode") == null)
        {
            HttpContext.Session.SetString("Passcode", "XXXXXXXXXXXXXX");
        }
        return View();
    }

    /////////////////////
    [HttpGet("generate")]
    public IActionResult Generate()
    {
        if (HttpContext.Session.GetInt32("Count") == null)
        {
            HttpContext.Session.SetInt32("Count", 0);
        }
        int? Count = HttpContext.Session.GetInt32("Count");
        HttpContext.Session.SetInt32("Count", (int)Count + 1);

        Random rand = new Random();
        string? Passcode = null;
        for (int i = 0; i < 14; i++) 
        {
        int randNum = rand.Next(2);
        if( randNum == 0) {
            int ascii_index = rand.Next(65, 91);
            // System.Console.WriteLine($"ascii_index: {ascii_index}");
            char RandUpperCase = Convert.ToChar(ascii_index);
            // System.Console.WriteLine($"RandUpperCase: {RandUpperCase}");
            Passcode = Passcode + RandUpperCase;
        } else {
            Passcode = Passcode + rand.Next(10);
        }
        // System.Console.WriteLine(Passcode);
        HttpContext.Session.SetString("Passcode", Passcode);
        }

        return RedirectToAction("Index");
    }
    /////////////////////
    [HttpGet("clear")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    public IActionResult Privacy()
    {
        return View();
    }
    /////////////////////

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
