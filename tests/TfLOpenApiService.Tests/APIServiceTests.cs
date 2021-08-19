using System;
using System.Threading.Tasks;
using System.Net.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using GenericServices;




namespace TfLOpenApiService.Tests
{
    [TestClass]
    public class APIServiceTests
    {
        private static IUriService UriSvc;
        private static IHttpClientService HttpClientSvcProvider;
        private static Uri ValidUri;
        private static RoadApiService RoadApiSvc;
        private static ApiServiceResponse<Road> ApiResponse;
        private static Road ValidRoad;

        private const string _validIResponse = "[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a24\",\"displayName\":\"A24\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.23393,51.33958],[-0.10287,51.49159]]\",\"envelope\":\"[[-0.23393,51.33958],[-0.23393,51.49159],[-0.10287,51.49159],[-0.10287,51.33958],[-0.23393,51.33958]]\",\"url\":\"/Road/a24\"}]";
        private const string ValidId = "A24";
        private const string StatusSeverity = "Good";
        private const string StatusSeverityDescription = "No Exceptional Delays";

        
        [ClassInitialize()]
        public static async Task ClassInitialize(TestContext context)
        {
            HttpClientSvcProvider = Mock.Of<IHttpClientService>();
            UriSvc = new RoadUriService();
            ValidUri = UriSvc.GetUri(ValidId);
            

            HttpResponseMessage mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            mockResponse.Content = new StringContent(_validIResponse);

            Mock.Get(HttpClientSvcProvider)
                .Setup(x => x.SendRequest(ValidUri))
                .ReturnsAsync(mockResponse);

            RoadApiSvc = new RoadApiService(UriSvc, HttpClientSvcProvider);
            ApiResponse = await RoadApiSvc.GetApiServiceResponse(ValidId);

            ValidRoad = ApiResponse.Instance;
        }

        [TestMethod]
        public void GetApiServiceResponse_ValidResponse_DesrializesObject()
        { 
            Assert.IsNotNull(ApiResponse.Instance);
        }

        [TestMethod]
        public void GetApiServiceResponse_ValidResponse_ReturnsCorrectRoadDisplayNam()
        {
            Assert.AreEqual(ValidRoad.DisplayName, ValidId);
        }

        [TestMethod]
        public void GetApiServiceResponse_ValidResponse_ReturnsCorrectRoadStatus()
        {
            Assert.AreEqual(ValidRoad.StatusSeverity, StatusSeverity);
        }

        [TestMethod]
        public void GetApiServiceResponse_ValidResponse_ReturnsCorrectRoadStatusDescription()
        {
            Assert.AreEqual(ValidRoad.StatusSeverityDescription, StatusSeverityDescription);
        }
    }
}
