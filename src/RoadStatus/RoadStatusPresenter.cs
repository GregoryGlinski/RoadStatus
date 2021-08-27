using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using TfLOpenApiService;

namespace TfLConsoleApp
{
    /// <summary>
    /// It implements a separation of concerns based on the MVP pattern except that here the 
    /// Model is referred to more in terms of a Service as it better describes its function.
    /// </summary>
    /// <param name="view"></param>
    /// <param name="apiService"></param>
    public class RoadStatusPresenter : IPresenter
    {
        private readonly IView _view;
        private IApiService<Road> _service;
        private ILogger<RoadStatusPresenter> _logger;

        /// <summary>
        /// The view and service (model) are injected, decoupling their specific implementations from this class.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="apiService"></param>
        public RoadStatusPresenter(IView view, IApiService<Road> apiService)
            : this(view, apiService, null)
        {

        }

        public RoadStatusPresenter(IView view, IApiService<Road> apiService, ILogger<RoadStatusPresenter> logger)
        {
            _view = view;
            _service = apiService;
            _logger = logger;
        }

        /// <summary>
        /// This is where the presenter does its work (presenting). This method is specific to the Road Status.
        /// There would possibly be scope here to refactor to an abstract base class if different API calls need to be added to the application. 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<int> Present(string[] args)
        {
            int returnCode = 0;

            try
            {
                if (ValidateArgs(args) == false)
                {
                    //Exit with error code = 1 immediately
                    return 1;
                }

                string id = GetIdFromArgs(args);

                ApiServiceResponse<Road> apiServiceResponse = await _service.GetApiServiceResponse(id);

                List<string> output = new List<string>();

                if (apiServiceResponse.ResponseStatusCode == ResponseStatusCode.OK && apiServiceResponse.Instance != null) //Could check further for validity of StatusSeverity and StatusSeverityDescription values.
                {
                    //Given a valid road ID is specified
                    //When the client is run
                    //Then the road ‘displayName’ should be displayed
                    //Then the road ‘statusSeverity’ should be displayed as ‘Road Status’
                    //Then the road ‘statusSeverityDescription’ should be displayed as ‘Road Status Description’

                    output.Add($"The status of the {id} is as follows");
                    output.Add($"\tRoad Status is {apiServiceResponse.Instance.StatusSeverity}");
                    output.Add($"\tRoad Status Description is {apiServiceResponse.Instance.StatusSeverityDescription}");

                    returnCode = 0;
                }
                else if (apiServiceResponse.ResponseStatusCode == ResponseStatusCode.NotFound && apiServiceResponse.Exception == null)
                {
                    //Given an invalid road ID is specified
                    //When the client is run
                    //Then the application should return an informative error

                    output.Add($"{id} is not a valid road");

                    //Then the application should exit with a non - zero System Error code

                    returnCode = 1;
                }
                else
                {
                    //This behaviour was not requested in the requirements but handles the case when unexpected issues prevent a road status being known
                    //and there is insufficient information to determine whether the supplied road ID is valid.

                    output.Add($"Unexpected behaviour occured. The status of the {id} is unknown.");

                    //The application should exit with a non - zero System Error code
                    returnCode = 1;
                }

                _view.Display(output);
            }
            catch(Exception e)
            {
                _logger?.LogError(e.Message);
                throw;
            }

            return returnCode;
        }

        /// <summary>
        /// Validates only first argument, checking that it is present and has some content, ignoring all others which are of no interest. 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool ValidateArgs(string[] args)
        {
            bool argsValid = true;

            if (args.Length == 0 || args[0].Trim().Length == 0)
            {
                _view.Display("A road id has either not been supplied or cannot be determined. The application will now exit.");
                argsValid = false;
            }

            return argsValid;
        }

        /// <summary>
        /// Overkill for current requirements but provides a structure to process further arguments later if this becomes necessary.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private string GetIdFromArgs(string[] args)
        {
            return args[0];
        }
    }
}
