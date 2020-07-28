using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoiceA.Data;
using ChoiceA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ChoiceA.Controllers
{
    [Authorize(Policy = "Admin")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private const string _defaultPassword = "123456";

        public StudentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.OrderBy(stud => stud.Group)
                                              .ThenBy(stud => stud.Name)
                                              .ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // Validation to protect from creating one student several times
        public JsonResult ValidateStudent(string Name, string Group)
        {
            if (_context.Student.Any(s => s.Name == Name && s.Group == Group))
                return Json("Such student already exists in database and cannot be added.");
            return Json(true);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Group")] Student student)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = student.Name, Email = student.Name };
                var result = await _userManager.CreateAsync(user, _defaultPassword);
                if (result.Succeeded)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    await _userManager.AddClaimAsync(user, new Claim("StudentId", student.Id.ToString()));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", result.Errors.First().Description);
                }
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Group")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
