using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;

namespace Utlanssystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UtlanssystemContext _context;

        public StudentsController(UtlanssystemContext context)
        {
            _context = context;
        }

/*-------------------------------------------------------------------------------------------
                          Bygg Index() — kun R i CRUD, studenter administreres ikke manuelt
---------------------------------------------------------------------------------------------
*/
        // GET: Students - viser en liste over alle studenter (kun visning, ingen CRUD).
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
    }
}