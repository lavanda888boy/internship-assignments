using Hospital.Application.Abstractions;
using Hospital.Application.Illnesses.Commands;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.Illnesses.Test.Commands.Test
{
    public class RegisterExistingIllnessTest
    {
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;

        public RegisterExistingIllnessTest()
        {
            _illnessRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, "flu", IllnessSeverity.MEDIUM)]
        public void Handle_ShouldReturnNewlyRegisteredIllness(int id, string name, IllnessSeverity illnessSeverity)
        {
            var illness = new Illness()
            {
                Id = id,
                Name = name,
                IllnessSeverity = illnessSeverity,
            };

            _illnessRepositoryMock.Setup(repo => repo.Create(It.IsAny<Illness>())).Returns(illness);

            var command = new RegisterExistingIllness(id, name, illnessSeverity);
            var handler = new RegisterExistingIllnessHandler(_illnessRepositoryMock.Object);

            Task<IllnessDto> illnessDto = handler.Handle(command, default);

            _illnessRepositoryMock.Verify(repo => repo.Create(It.IsAny<Illness>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(id, illnessDto.Result.Id);
                Assert.Equal(name, illnessDto.Result.Name);
                Assert.Equal(illnessSeverity, illnessDto.Result.IllnessSeverity);
            });
        }
    }
}
