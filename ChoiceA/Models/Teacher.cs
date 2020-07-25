using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChoiceA.Models
{
    public class Teacher
    {
        public int Id { set; get; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [RegularExpression("[A-Z][a-z]+[ ][A-Z][a-z]+")]
        public string Name { set; get; }
        //
        public List<Discipline> Disciplines { set; get; }
    }
}
