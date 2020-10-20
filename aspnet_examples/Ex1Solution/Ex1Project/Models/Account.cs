using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Ex1Project.Models 
{
    public class Account
    {
        [Required]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(0,10)]
        public int? Count {get; set;}

        public IEnumerable<int> CountList {get; set;}

        public override string ToString()
        {
            return $"name={Name}, start_date={StartDate}, end_date={EndDate}, count={Count}";
        }
    }
}