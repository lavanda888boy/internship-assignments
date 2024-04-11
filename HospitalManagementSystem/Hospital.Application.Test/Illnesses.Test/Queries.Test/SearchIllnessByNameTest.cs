using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Queries;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.Illnesses.Test.Queries.Test
{
    public class SearchIllnessByNameTest
    {
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;

        public SearchIllnessByNameTest()
        {
            _illnessRepositoryMock = new();
        }

        [Theory]
        [InlineData("cough")]
        public void Handle_ShouldReturnIllnessDto(string name)
        {
            var illnesses = new List<Illness>()
            {
                new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM },
                new Illness { Id = 2, Name = "cough", IllnessSeverity = IllnessSeverity.LOW }
            };

            var filteredIllnesses = illnesses.Where(i => i.Name == name).ToList();

            _illnessRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>())).Returns(filteredIllnesses);

            var command = new SearchIllnessByName(name);
            var handler = new SearchIllnessByNameHandler(_illnessRepositoryMock.Object);

            Task<IllnessDto> illnesDto = handler.Handle(command, default);

            _illnessRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal("cough", illnesDto.Result.Name);
                Assert.Equal(IllnessSeverity.LOW, illnesDto.Result.IllnessSeverity);
            });
        }

        [Theory]
        [InlineData("heart attack")]
        public async Task Handle_ShouldThrowException_NoIllnessWithSuchName(string name)
        {
            var illnesses = new List<Illness>()
            {
                new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM },
                new Illness { Id = 2, Name = "cough", IllnessSeverity = IllnessSeverity.LOW }
            };

            var filteredIllnesses = illnesses.Where(i => i.Name == name).ToList();

            _illnessRepositoryMock.Setup(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>())).Returns(filteredIllnesses);

            var command = new SearchIllnessByName(name);
            var handler = new SearchIllnessByNameHandler(_illnessRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _illnessRepositoryMock.Verify(repo => repo.SearchByProperty(It.IsAny<Func<Illness, bool>>()), Times.Once);
        }
    }
}
