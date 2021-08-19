using System.Threading.Tasks;

namespace TfLConsoleApp
{
    public interface IPresenter
    {
        Task<int> Present(string[] args);
    }
}
