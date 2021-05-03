using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.BDDTests.src
{
    public class Calculator
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add()
        {
            //throw new NotImplementedException();
            return FirstNumber + SecondNumber;
        }
    }
}
