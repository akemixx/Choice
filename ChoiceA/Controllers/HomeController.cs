using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChoiceA.Models;
using Microsoft.AspNetCore.Authorization;
using ChoiceA.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace ChoiceA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student's list
        public async Task<IActionResult> Index()
        {
            var _studentId = User.Claims.FirstOrDefault(c => c.Type == "StudentId");
            if (_studentId != null)
            {
                return RedirectToAction("Index", "StudDiscs", new { studentId = Convert.ToInt32(_studentId.Value) });
            }
            return View(await _context.Student.OrderBy(stud => stud.Group)
                                              .ThenBy(stud => stud.Name)
                                              .ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
