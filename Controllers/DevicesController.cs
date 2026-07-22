// Denne klassen skal vise en liste over enheter.
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Devices.ToListAsync());
        }

/*-------------------------------------------------------------------------------------------
                          Bygg Create() — bokstav C i CRUD
---------------------------------------------------------------------------------------------
*/
        // GET: Devices/Create - viser tomt skjema for å legge til ny enhet i DB.
        [Authorize(Roles = "Admin")] 
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
        [Authorize(Roles = "Admin")]
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

/*-------------------------------------------------------------------------------------------
                            Bygg Edit() — bokstav U i CRUD
---------------------------------------------------------------------------------------------
*/
        // GET: Devices/Edit/5 - viser skjema med eksisterende data for redigering.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }
        // POST: Devices/Edit/5 - mottar endrede data og oppdaterer enheten i databasen.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DeviceType,ModelName,Specifications,IsAvailable")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Enheten '{device.Name}' ble oppdatert!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }
/*-------------------------------------------------------------------------------------------
                    Hjelpemetode brukt av Edit() ved samtidighetsfeil
---------------------------------------------------------------------------------------------
*/
        // Sjekker om enheten fortsatt finnes i databasen.
        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
/*-------------------------------------------------------------------------------------------
                          Bygg Delete() — bokstav D i CRUD
---------------------------------------------------------------------------------------------
*/
        // GET: Devices/Delete/5 - viser bekreftelsesside før sletting.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id); // Henter enheten fra databasen basert på ID.PK
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }   
        // POST: Denne metoden utfører selve slettingen etter bekreftelse fra brukeren.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                TempData["SuccessMessage"] = $"Enheten '{device.Name}' ble slettet!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
/*-------------------------------------------------------------------------------------------
                    Bygg Available() — viser kun ledige enheter
---------------------------------------------------------------------------------------------
*/
        // GET: Devices/Available - viser enheter som er tilgjengelige for utlån.
        public async Task<IActionResult> Available()
        {
            var availableDevices = _context.Devices.Where(d => d.IsAvailable == true);
            return View(await availableDevices.ToListAsync());
        }
    }  
}