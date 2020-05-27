namespace Choice.Models
{
    public class SelectDisciplineViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }

        public SelectDisciplineViewModel() { }

        public SelectDisciplineViewModel(int id, string title, bool selected = false)
        {
            Id = id;
            Title = title;
            Selected = selected;
        }
    }
}
