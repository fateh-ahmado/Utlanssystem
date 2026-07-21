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
            var availableDevices = _context.Devices.Where(d => d.IsAvailable == true);
            var availableStudents = _context.Students.Where(s => s.HasActiveLoan == false);

            ViewBag.DeviceId = new SelectList(availableDevices, "Id", "Name");
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

            if (device != null && student != null)
            {
                loan.LoanDate = DateTime.Now;

                device.IsAvailable = false;
                student.HasActiveLoan = true;

                _context.Add(loan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Lån registrert: {student.FirstName} {student.LastName} har lånt {device.Name}!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }
    }
    
}