using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace ScuolaApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        // Vista principale
        public IActionResult Index()
        {
            return View();
        }

        // Ritorna la Partial View con la lista (usata da AJAX)
        public async Task<IActionResult> ListaParziale(string search = null)
        {
            var query = _context.Students.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Nome.Contains(search) ||
                    s.Cognome.Contains(search) ||
                    s.Email.Contains(search));
            }

            var student = await query.OrderBy(s => s.Cognome).ThenBy(s => s.Nome).ToListAsync();
            return PartialView("_StudentListPartial", student);
        }
    }
}
