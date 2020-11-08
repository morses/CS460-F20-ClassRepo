using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AJAX_Example.Models
{
    public class RandomNumberObject
    {
        public string Message { get; set; }
        public int Count { get; set; }
        public int Max { get; set; }
        public IEnumerable<int> Domain { get; set; }
        public IEnumerable<int> Range { get; set; }
         
        public RandomNumberObject(string message, int count, int max)
        {
            this.Message = message;
            this.Count = count;
            this.Max = max;

            Random generator = new Random();
            Domain = Enumerable.Range(1, count);
            Range = Enumerable.Range(1, count).Select(x => generator.Next(Max));
        }
    }
}