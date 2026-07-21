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

/*-------------------------------------------------------------------------------------------
                          Bygg Index() — bokstav R i CRUD
---------------------------------------------------------------------------------------------
*/
        // GET: Devices - viser en liste over alle enheter.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Devices.ToListAsync());
        }

/*-------------------------------------------------------------------------------------------
                          Bygg Create() — bokstav C i CRUD
---------------------------------------------------------------------------------------------
*/
        // GET: Devices/Create - viser tomt skjema for å legge til ny enhet i DB.
        public IActionResult Create()
        {
            return View();
        }

/*-------------------------------------------------------------------------------------------
                          Motta og lagre skjema — POST-delen av Create()
---------------------------------------------------------------------------------------------
*/
        // POST: Devices/Create - mottar utfylt skjema og lagrer ny enhet i databasen.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DeviceType,ModelName,Specifications,IsAvailable")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Enheten '{device.Name}' ble lagt til!"; // Legger til en midlertidig bekreftelsesmelding i TempData
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }
    }  
}