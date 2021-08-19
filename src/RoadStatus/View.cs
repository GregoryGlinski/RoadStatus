using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfLConsoleApp
{
    //
    //()
    /// <summary>
    /// A wrapper around Console.Writeline to display a single line of text or a list of lines.
    /// Overkill for the current requirements as this could be handled by Console.Writeline but it separates the View implementation from the Presenter (if this can be called View).
    /// </summary>
    public class View : IView
    {
        public void Display(List<string> output)
        {
            foreach(string line in output)
            {
                Console.WriteLine(line);
            }
        }

        public void Display(string output)
        {
            Console.WriteLine(output);
        }
    }
}
