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

        [Fact]
        public async Task Post_Given_Valid_Request_Should_Call_Database_Service()
        {
            // Arrange
            var controller = new MedicationRequestController(_mockDatabaseService.Object);

            var requestModel = new MedicationRequestModel
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
            var response = await controller.Post(requestModel) as CreatedResult;

            // Assert
            _mockDatabaseService.Verify(
            s => s.AddMedicationRequest(It.Is<MedicationRequest>(mr =>
                mr.ClinicianReference == requestModel.ClinicianReference &&
                mr.PatientReference == requestModel.PatientReference &&
                mr.MedicationReference == requestModel.MedicationReference &&
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
            var controller = new MedicationRequestController(_mockDatabaseService.Object);

            var medicationRequest = new MedicationRequestModel 
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
            var response = await controller.Post(medicationRequest) as CreatedResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.Created, response.StatusCode);
        }
    }
}