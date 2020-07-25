using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChoiceA.Models
{
    public class Student
    {
        public int Id { set; get; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [RegularExpression("[A-Z][a-z]+[ ][A-Z][a-z]+")]
        [Remote("ValidateStudent", "Students", AdditionalFields="Group")]
        public string Name { set; get; }
        [Required]
        public string Group { set; get; }
        //
        public List<StudDisc> StudDiscs { set; get; }
    }
}
