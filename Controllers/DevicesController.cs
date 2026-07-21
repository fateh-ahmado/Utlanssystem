// Denne klassen skal vise en liste over enheter.
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;
using Utlanssystem.Models;

namespace Utlanssystem.Controllers
{
    public class DevicesController : Controller
    {
        private readonly UtlanssystemContext _context;

        public DevicesController(UtlanssystemContext context)
        {
            _context = context;
        }
        // GET: Devices- viser en liste over alle enheter (bokstav R i CRUD).
        public async Task<IActionResult> Index()
        {
            return View(await _context.Devices.ToListAsync());
        }
    }
}