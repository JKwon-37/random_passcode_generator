#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscodeGenerator.Models;

namespace RandomPasscodeGenerator.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpPost("generatePw")]
    public IActionResult GeneratePasscode()
    {   
        int? count = HttpContext.Session.GetInt32("Counter");
        if (count != null)
        {
            count++;
            HttpContext.Session.SetInt32("Counter", (int)count);
        } else
        {
            HttpContext.Session.SetInt32("Counter", 1);
        }
        Random rand = new Random();
        string randomChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
        int length = 14;
        string newCode = "";

        for (var i=0; i<length; i++)
        {
            int runner = rand.Next(randomChar.Length);
            newCode += randomChar[runner];
            // Console.WriteLine(newCode);
        }

        HttpContext.Session.SetString("Password", newCode);
        
        return View("Index");
    }

    [HttpGet("clear")]
    public IActionResult Restart()
    {
        HttpContext.Session.Remove("Counter");
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
