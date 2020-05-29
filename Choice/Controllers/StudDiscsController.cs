using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Choice.Data;
using Choice.Models;
using Microsoft.AspNetCore.Routing;

namespace Choice.Controllers
{
    public class StudDiscsController : Controller
    {
        private readonly AppDbContext _context;

        public StudDiscsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: List of disciplines with checkboxes
        public ActionResult Index(int studentId)
        {
            IEnumerable<int> selectedByUserDisciplines = _context.StudDisc.Where(row => row.StudentId == studentId)
                                                                    .Select(row => row.DisciplineId);
            var model = new DisciplineSelectionViewModel();
            foreach(var disc in _context.Discipline)
            {
                var selectDiscipline = new SelectDisciplineViewModel(disc.Id, disc.Title);
                if (selectedByUserDisciplines.Contains(disc.Id))
                {
                    selectDiscipline.Selected = true;
                }
                model.Disciplines.Add(selectDiscipline);
            }
            model.Disciplines = model.Disciplines.OrderByDescending(disc => disc.Selected)
                                                 .ThenBy(disc => disc.Title)
                                                 .ToList();

            model.Student = _context.Student.SingleOrDefault(stud => stud.Id == studentId);
            return View(model);
        }

        // POST: Changes to selected disciplines for concrete student
        [HttpPost]
        public async Task<IActionResult> FormSubmit(DisciplineSelectionViewModel model)
        {
            IList<SelectDisciplineViewModel> newSelections = model.Disciplines;
            Student student = model.Student;
            IEnumerable<int> selectedDiscs = _context.StudDisc.Where(row => row.StudentId == student.Id)
                                                              .Select(row => row.DisciplineId);
            foreach (var disc in newSelections)
            {
                if(disc.Selected && !selectedDiscs.Contains(disc.Id))
                {
                    _context.StudDisc.Add(new StudDisc { DisciplineId = disc.Id, StudentId = student.Id });
                }
                else if(!disc.Selected && selectedDiscs.Contains(disc.Id))
                {
                    _context.StudDisc.Remove(new StudDisc { DisciplineId = disc.Id, StudentId = student.Id });
                }
            }
            await _context.SaveChangesAsync();

            // redirect to Home Page
            //return RedirectToAction("Index", new RouteValueDictionary(
            //                         new { controller = "Home", action = "Index" }));

            // renew page
            return RedirectToAction("Index", new { studentId = student.Id });
        }
    }
}
