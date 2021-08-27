using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            IServiceCollection services;
            ServiceProvider serviceProvider = null;

            try
            {
                // Configure services
                services = ConfigureServices();
                // Create provider
                serviceProvider = services.BuildServiceProvider();
                // Run application
                returnCode = await serviceProvider.GetService<RoadStatusPresenter>().Present(args);

                //Previous code without using using Microsoft.Extensions.DependencyInjection moved to method RunWithoutDependencyInjectionFramework(string[] args)
            }
            catch (Exception e)
            {
                string message = "\nAn unhandled exception has occured.";
                message += $"\nMessage :{e.Message} ";
                message += $"\nThe application will now exit.";

                try
                {
                    //Only place to handle a general unhandled System.Exception
                    serviceProvider.GetService<ILogger<Program>>()?.LogError(message);
                }
                catch
                {
                    //Last resort in case dependency injection, service configuration or logging fails:
                    Console.WriteLine(message);
                }
                finally
                {
                    returnCode = 1;
                }
            }

            return returnCode;
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddDebug();
            });

            services.AddTransient<IUriService, RoadUriService>();
            services.AddTransient<IView, View>();
            services.AddTransient<IHttpClientService, HttpClientServiceProvider>();
            services.AddTransient(typeof(IApiService<Road>), typeof(RoadApiService));

            //Register entry point
            services.AddTransient<RoadStatusPresenter>();

            return services;
        }

        /// <summary>
        /// Previous code without using using Microsoft.Extensions.DependencyInjection or logging
        /// </summary>
        /// <returns></returns>
        private static async Task<int> RunWithoutDependencyInjectionFramework(string[] args)
        {
            //Create Service (Model)
            IApiService<Road> service = new RoadApiService(new RoadUriService(), new HttpClientServiceProvider());
            //Create View
            IView view = new View();
            //Create Presenter, inject view and service (model) and start presenting
            RoadStatusPresenter presenter = new RoadStatusPresenter(view, service);

            return await presenter.Present(args);
        }
    }
}
