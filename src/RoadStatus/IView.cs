using System.Collections.Generic;

namespace TfLConsoleApp
{
    public interface IView
    {
        void Display(List<string> output);
        void Display(string output);
    }
}
