using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Queries;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Treatments.Test.Queries.Test
{
    public class SearchTreatmentByPrescribedMedicineTest
    {
        private readonly Mock<ITreatmentRepository> _treatmentRepositoryMock;

        public SearchTreatmentByPrescribedMedicineTest()
        {
            _treatmentRepositoryMock = new();
        }

        [Theory]
        [InlineData("panadol")]
        public void Handle_ShouldReturnListOfTreatmentDtos(string medicine)
        {
            var treatments = new List<Treatment>
            {
                new Treatment { Id = 1, PrescribedMedicine = "panadol", TreatmentDuration = new TimeSpan(7, 0, 0) },
                new Treatment { Id = 2, PrescribedMedicine = "suprastin", TreatmentDuration = new TimeSpan(3, 0, 0) },
                new Treatment { Id = 3, PrescribedMedicine = "panadol", TreatmentDuration = new TimeSpan(4, 0, 0) },
            };

            var filteredTreatments = treatments.Where(t => t.PrescribedMedicine == medicine).ToList();

            _treatmentRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Treatment, bool>>())).Returns(filteredTreatments);

            var command = new SearchTreatmentByPrescribedMedicine(medicine);
            var handler = new SearchTreatmentByPrescribedMedicineHandler(_treatmentRepositoryMock.Object);

            Task<List<TreatmentDto>> treatmentDtos = handler.Handle(command, default);

            _treatmentRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Treatment, bool>>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(2, treatmentDtos.Result.Count);
                Assert.Equal("panadol", treatmentDtos.Result[0].PrescribedMedicine);
                Assert.Equal("panadol", treatmentDtos.Result[1].PrescribedMedicine);
            });
        }

        [Theory]
        [InlineData("nurofen")]
        public async Task Handle_ShouldThrowException_NoTreatmentsWithSuchPrescribedMedicine(string medicine)
        {
            var treatments = new List<Treatment>
            {
                new Treatment { Id = 1, PrescribedMedicine = "panadol", TreatmentDuration = new TimeSpan(7, 0, 0) },
                new Treatment { Id = 2, PrescribedMedicine = "suprastin", TreatmentDuration = new TimeSpan(3, 0, 0) },
                new Treatment { Id = 3, PrescribedMedicine = "panadol", TreatmentDuration = new TimeSpan(4, 0, 0) },
            };

            var filteredTreatments = treatments.Where(t => t.PrescribedMedicine == medicine).ToList();

            _treatmentRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Treatment, bool>>())).Returns(filteredTreatments);

            var command = new SearchTreatmentByPrescribedMedicine(medicine);
            var handler = new SearchTreatmentByPrescribedMedicineHandler(_treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _treatmentRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Treatment, bool>>()), Times.Once);
        }
    }
}
