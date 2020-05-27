using System.Collections.Generic;

namespace Choice.Models
{
    public class DisciplineSelectionViewModel
    {
        public int StudentId { set; get; }
        public IList<SelectDisciplineViewModel> Disciplines { set; get; }
        
        public DisciplineSelectionViewModel()
        {
            Disciplines = new List<SelectDisciplineViewModel>();
        }

        public DisciplineSelectionViewModel(int _studentId) : base()
        {
            StudentId = _studentId;
        }
    }
}
