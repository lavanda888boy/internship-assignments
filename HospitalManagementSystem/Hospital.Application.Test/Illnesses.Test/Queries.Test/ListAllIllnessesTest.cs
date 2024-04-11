using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Queries;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.Illnesses.Test.Queries.Test
{
    public class ListAllIllnessesTest
    {
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;

        public ListAllIllnessesTest()
        {
            _illnessRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfTreatmentDtos()
        {
            var illnesses = new List<Illness>
            {
                new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM },
                new Illness { Id = 2, Name = "hiv", IllnessSeverity = IllnessSeverity.HIGH },
            };

            _illnessRepositoryMock.Setup(repo => repo.GetAll()).Returns(illnesses);

            var command = new ListAllIllnesses();
            var handler = new ListAllIllnessesHandler(_illnessRepositoryMock.Object);

            Task<List<IllnessDto>> illnessDtos = handler.Handle(command, default);

            _illnessRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(2, illnessDtos.Result.Count);
                Assert.Equal("flu", illnessDtos.Result[0].Name);
                Assert.Equal(IllnessSeverity.HIGH, illnessDtos.Result[1].IllnessSeverity);
            });
        }
    }
}
