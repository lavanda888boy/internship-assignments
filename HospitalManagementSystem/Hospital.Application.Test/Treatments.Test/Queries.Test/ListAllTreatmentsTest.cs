using Hospital.Application.Abstractions;
using Hospital.Application.Treatments.Queries;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Treatments.Test.Queries.Test
{
    public class ListAllTreatmentsTest
    {
        private readonly Mock<ITreatmentRepository> _treatmentRepositoryMock;

        public ListAllTreatmentsTest()
        {
            _treatmentRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfTreatmentDtos()
        {
            var treatments = new List<Treatment>
            {
                new Treatment { Id = 1, PrescribedMedicine = "panadol", TreatmentDuration = new TimeSpan(7, 0, 0) },
                new Treatment { Id = 2, PrescribedMedicine = "suprastin", TreatmentDuration = new TimeSpan(3, 0, 0) },
                new Treatment { Id = 3, PrescribedMedicine = "nurofen", TreatmentDuration = new TimeSpan(4, 0, 0) },
            };

            _treatmentRepositoryMock.Setup(repo => repo.GetAll()).Returns(treatments);

            var command = new ListAllTreatments();
            var handler = new ListAllTreatmentsHandler(_treatmentRepositoryMock.Object);

            Task<List<TreatmentDto>> treatmentDtos = handler.Handle(command, default);

            _treatmentRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(3, treatmentDtos.Result.Count);
                Assert.Equal("suprastin", treatmentDtos.Result[1].PrescribedMedicine);
                Assert.Equal(new TimeSpan(4, 0, 0), treatmentDtos.Result[2].TreatmentDuration);
            });
        }
    }
}
