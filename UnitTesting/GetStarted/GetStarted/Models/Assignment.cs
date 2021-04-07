using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Models
{
    public class Assignment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,5)]
        public int Priority { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1,4} \d{3}[A-Z]?$",ErrorMessage = "Course string did not match the required format")]
        public string Course { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public bool Completed { get; set; }
        public bool Submitted { get; set; }

        public Assignment(){}

        public int DaysUntilDue()
        {
            return 0;
        }

    }
}
