using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChoiceA.Models
{
    public class Discipline
    {
        public int Id { set; get; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { set; get; }
        public string Annotation { set; get; }
        [Required]
        public int TeacherId { set; get; }
        //
        public Teacher Teacher { set; get; }
        public List<StudDisc> StudDiscs { set; get; }
    }
}
