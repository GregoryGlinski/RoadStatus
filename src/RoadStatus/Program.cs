using System;
using System.Threading.Tasks;

using GenericServices;

using TfLOpenApiService;

namespace TfLConsoleApp
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            //Return code set to 1 (error) as the default value as this can only be set to zero if that is what the call to presenter.Present returns.
            int returnCode = 1;

            try
            {
                //There is scope for some creational patterns such as factories

                //Create Service (Model)
                IApiService<Road> service = new RoadApiService(new RoadUriService(), new HttpClientServiceProvider());

                //Create View
                IView view = new View();

                //Create Presenter, inject view and service (model) and start presenting
                RoadStatusPresenter presenter = new RoadStatusPresenter(view, service);

                returnCode = await presenter.Present(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn exception has occured.");
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("The application will now exit.");
                returnCode = 1;
            }

            return returnCode;
        }
    }
}
