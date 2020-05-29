using System.Collections.Generic;

namespace Choice.Models
{
    public class DisciplineSelectionViewModel
    {
        public Student Student { get; set; }
        public IList<SelectDisciplineViewModel> Disciplines { set; get; }
        
        public DisciplineSelectionViewModel()
        {
            Disciplines = new List<SelectDisciplineViewModel>();
        }

        public DisciplineSelectionViewModel(Student student) : base()
        {
            Student = student;
        }
    }
}
