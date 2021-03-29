using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Utility
{
    public class StringUtilities
    {
        // Refactoring our code to make it testable
        // Move it out into its own method
        // Then put that method someplace that makes sense
        // Lastly, fix errors
        //[NonAction]
        public static string Capitalize(string input)
        {
            string capitalized = char.ToUpper(input[0]) + input.Substring(1);
            return capitalized;
        }
    }
}
