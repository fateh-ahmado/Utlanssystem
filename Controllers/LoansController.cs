using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;

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
    }
}