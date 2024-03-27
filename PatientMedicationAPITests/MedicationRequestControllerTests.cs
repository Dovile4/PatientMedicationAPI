using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientMedicationAPI.Controllers;
using PatientMedicationAPI.Models.API;
using PatientMedicationAPI.Models.Database;
using PatientMedicationAPI.Services;
using System.Net;

namespace PatientMedicationAPITests
{
    public class MedicationRequestControllerTests
    {
        private readonly Mock<IDatabaseService> _mockDatabaseService = new Mock<IDatabaseService>();
        private readonly MedicationRequestController _controller;

        public MedicationRequestControllerTests()
        {
            _controller = new MedicationRequestController(_mockDatabaseService.Object);
        }

        [Fact]
        public async Task Post_Given_Valid_Request_Should_Call_Database_Service()
        {
            // Arrange
            var requestModel = new AddMedicationRequestModel
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                PrescribedDate = new DateOnly(),
                Status = "Active",
                ClinicianReference = 1,
                PatientReference = 1,
                MedicationReference = 1,
                Frequency = "twice a day",
                Reason = "test"
            };           

            // Act
            var response = await _controller.Post(requestModel) as CreatedResult;

            // Assert
            _mockDatabaseService.Verify(
            s => s.AddMedicationRequest(It.Is<MedicationRequest>(mr =>
                mr.ClinicianId == requestModel.ClinicianReference &&
                mr.PatientId == requestModel.PatientReference &&
                mr.MedicationId == requestModel.MedicationReference &&
                mr.Frequency == requestModel.Frequency &&
                mr.Reason == requestModel.Reason &&
                mr.PrescribedDate == requestModel.PrescribedDate &&
                mr.StartDate == requestModel.StartDate &&
                mr.EndDate == requestModel.EndDate &&
                mr.Status == requestModel.Status)),
            Times.Once);
        }

        [Fact]
        public async Task Post_Given_Valid_Request_Should_Return_201_Created()
        {
            // Arrange
            var medicationRequest = new AddMedicationRequestModel 
            { 
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                PrescribedDate = new DateOnly(),
                Status = "Active",
                ClinicianReference = 1,
                PatientReference = 1,
                MedicationReference = 1,
                Frequency = "twice a day",
                Reason = "test"
            };

            _mockDatabaseService.Setup(s => s.AddMedicationRequest(It.IsAny<MedicationRequest>())).ReturnsAsync(new MedicationRequest());

            // Act
            var response = await _controller.Post(medicationRequest) as CreatedResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_Given_RequestIs_Null_Should_Return_400_BadRequest()
        {
            // Arrange
            _mockDatabaseService.Setup(s => s.AddMedicationRequest(It.IsAny<MedicationRequest>())).ReturnsAsync(new MedicationRequest());

            // Act
            var response = await _controller.Post(default(AddMedicationRequestModel));

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            var result = (BadRequestObjectResult)response;
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Request cannot be null", result.Value);
        }

        [Fact]
        public async Task Get_Given_All_Valid_Parameters_Should_Call_Database_Service()
        {
            // Arrange
            const int id = 1;
            const string status = "Active";
            DateOnly startDate = new DateOnly(2024, 03, 26);
            DateOnly endDate = new DateOnly(2024, 03, 27);

            // Act
            // Assert
            await Record.ExceptionAsync(async () => {
                await _controller.Get(id, status, startDate, endDate);

                _mockDatabaseService.Verify(s => s.GetFilteredMedicationRequests(id, status, startDate, endDate), Times.Once);

            }
            );
        }

        [Fact]
        public async Task Get_Db_Returns_Null_Should_Return_400_BadRequest()
        {
            // Arrange
            const int id = 1;
            const string status = "Active";
            DateOnly startDate = new DateOnly(2024, 03, 26);
            DateOnly endDate = new DateOnly(2024, 03, 27);            

            // Act
            var response = await _controller.Get(id, status, startDate, endDate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            var result = (BadRequestObjectResult)response;
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("An error occured when trying to retrieve medication requests", result.Value);
        }

        [Fact]
        public async Task Get_Db_Returns_Empty_Results_Should_Return_404_NotFound()
        {
            // Arrange
            const int id = 1;
            const string status = "Active";
            DateOnly startDate = new DateOnly(2024, 03, 26);
            DateOnly endDate = new DateOnly(2024, 03, 27);

            _mockDatabaseService.Setup(s => s.GetFilteredMedicationRequests(id, status, startDate, endDate)).ReturnsAsync(new List<GetMedicationRequestsResponse>());
            // Act
            var response = await _controller.Get(id, status, startDate, endDate);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
            var result = (NotFoundObjectResult)response;
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Could not find any medication requests with filters provided", result.Value);
        }
    }
} 