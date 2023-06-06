using FakeItEasy;
using MapsterMapper;
using ToDo.App.Application.Commands.ToDoList;
using ToDo.App.Application.Handlers.ToDoList;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.Enums;
using ToDo.App.Domain.Repositories.UnitOfWork;
using ToDo.App.Domain.ValueObjects;

namespace ToDo.App.Test.Application
{
    public class ToDoCommandHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoCommandHandlerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<Mapper>();
        }

        [Fact]
        public void Add_ToDo_Command_Handler()
        {
            //Arrange
            var command = new AddToDoListCommand(
                "",
                Tag.Bussines,
                new[] {
                    ToDoItem.Create("Test",
                    "Test",
                    DateTimeOffset.UtcNow,
                    new PriorityLevel(2))
                },
                1);
            var handler = new AddToDoListCommandHandler(_mapper, _unitOfWork);
            // Act
            var result = handler.Handle(command, new CancellationTokenSource().Token);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ToDo_Command_Handler()
        {
            //Arrange
            var command = new EditToDoListCommand(
               1,
               "",
               Tag.Bussines,
               new[] {
                    ToDoItem.Create("Test",
                    "Test",
                    DateTimeOffset.UtcNow,
                    new PriorityLevel(2))
                      },
               1);
            var handler = new EditToDoListCommandHandler(_mapper, _unitOfWork);
            // Act
            var result = handler.Handle(command, new CancellationTokenSource().Token);
            // Assert
            Assert.NotNull(result);
        }
    }
}
