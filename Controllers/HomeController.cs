using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Utlanssystem.Data;
using Utlanssystem.Models;

namespace Utlanssystem.Controllers;

public class HomeController : Controller
{
    private readonly UtlanssystemContext _context;

    public HomeController(UtlanssystemContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.TotalDevices = _context.Devices.Count();
        ViewBag.AvailableDevices = _context.Devices.Count(d => d.IsAvailable == true);
        ViewBag.ActiveLoans = _context.Loans.Count();
        ViewBag.TotalStudents = _context.Students.Count();
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
