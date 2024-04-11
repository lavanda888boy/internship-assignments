using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Queries;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Illnesses.Test.Queries.Test
{
    public class SearchIllnessBySeverityTest
    {
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;

        public SearchIllnessBySeverityTest()
        {
            _illnessRepositoryMock = new();
        }

        [Theory]
        [InlineData(IllnessSeverity.LOW)]
        public void Handle_ShouldReturnIllnessDto(IllnessSeverity severity)
        {
            var illnesses = new List<Illness>()
            {
                new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM },
                new Illness { Id = 2, Name = "cough", IllnessSeverity = IllnessSeverity.LOW },
                new Illness { Id = 3, Name = "headache", IllnessSeverity = IllnessSeverity.LOW }
            };

            var filteredIllnesses = illnesses.Where(i => i.IllnessSeverity == severity).ToList();

            _illnessRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>())).Returns(filteredIllnesses);

            var command = new SearchIllnessBySeverity(severity);
            var handler = new SearchIllnessBySeverityHandler(_illnessRepositoryMock.Object);

            Task<List<IllnessDto>> illnessDtos = handler.Handle(command, default);

            _illnessRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(2, illnessDtos.Result.Count);
                Assert.Equal("cough", illnessDtos.Result[0].Name);
                Assert.Equal("headache", illnessDtos.Result[1].Name);
            });
        }

        [Theory]
        [InlineData(IllnessSeverity.HIGH)]
        public async Task Handle_ShouldThrowException_NoIllnessWithSuchSeverity(IllnessSeverity severity)
        {
            var illnesses = new List<Illness>()
            {
                new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM },
                new Illness { Id = 2, Name = "cough", IllnessSeverity = IllnessSeverity.LOW }
            };

            var filteredIllnesses = illnesses.Where(i => i.IllnessSeverity == severity).ToList();

            _illnessRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>())).Returns(filteredIllnesses);

            var command = new SearchIllnessBySeverity(severity);
            var handler = new SearchIllnessBySeverityHandler(_illnessRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _illnessRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>()), Times.Once);
        }
    }
}
