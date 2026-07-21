using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utlanssystem.Models;


namespace Utlanssystem.Controllers
{
    public class LoansController : Controller
    {
        private readonly UtlanssystemContext _context;

        public LoansController(UtlanssystemContext context)
        {
            _context = context;
        }

/*-------------------------------------------------------------------------------------------
                          Bygg Index() — oversikt over aktive lån
---------------------------------------------------------------------------------------------
*/
        // GET: Loans - viser alle aktive lån, med tilhørende enhet og student.
        public async Task<IActionResult> Index()
        {
            var loans = _context.Loans
                .Include(l => l.Device)
                .Include(l => l.Student);

            return View(await loans.ToListAsync());
        }
/*-------------------------------------------------------------------------------------------
                    Bygg Create() — skjema for å registrere nytt lån
---------------------------------------------------------------------------------------------
*/
        // GET: Loans/Create - viser skjema med nedtrekkslister for ledige enheter og studenter uten lån.
        public IActionResult Create()
        {
            var allDevices = _context.Devices;
            var availableStudents = _context.Students
                .Where(s => _context.Loans.Count(l => l.StudentId == s.Id) < 2);

            ViewBag.DeviceId = new SelectList(allDevices, "Id", "Name");
            ViewBag.StudentId = new SelectList(availableStudents, "Id", "FirstName");

            return View();
        }

        // POST: Loans/Create - mottar valgt enhet og student, oppretter nytt lån.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeviceId,StudentId")] Loan loan)
        {
            var device = await _context.Devices.FindAsync(loan.DeviceId);
            var student = await _context.Students.FindAsync(loan.StudentId);

            if (device == null || student == null)
            {
                TempData["ErrorMessage"] = "Ugyldig enhet eller student valgt.";
                return RedirectToAction(nameof(Create));
            }

            // Forretningsregel 1: enheten må være tilgjengelig
            if (!device.IsAvailable)
            {
                TempData["ErrorMessage"] = $"'{device.Name}' er allerede utlånt. Velg en annen enhet.";
                return RedirectToAction(nameof(Create));
            }

            // Forretningsregel 2: studenten kan ha maks 2 aktive lån samtidig
            int activeLoanCount = _context.Loans.Count(l => l.StudentId == student.Id);
            if (activeLoanCount >= 2)
            {
                TempData["ErrorMessage"] = $"{student.FirstName} {student.LastName} har allerede 2 aktive lån (maks tillatt).";
                return RedirectToAction(nameof(Create));
            }

            loan.LoanDate = DateTime.Now;
            device.IsAvailable = false;
            student.HasActiveLoan = true;

            _context.Add(loan);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Lån registrert: {student.FirstName} {student.LastName} har lånt {device.Name}!";
            return RedirectToAction(nameof(Index));
        }
    }
    
}