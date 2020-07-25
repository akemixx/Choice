using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChoiceA.Models;
using Microsoft.AspNetCore.Authorization;
using ChoiceA.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return View(await _context.Student.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
