using ToDo.App.Domain.Entities;
using ToDo.App.Domain.Enums;
using ToDo.App.Domain.Exceptions;
using ToDo.App.Domain.ValueObjects;

namespace ToDo.App.Test.Domain
{
    public class TodoListTests
    {

        [Fact]
        public void Create_New_Instance()
        {
            //Arrange
            string Title = "New task";
            Tag Tagtype = Tag.Entertainment;
            ToDoItem todoItem = ToDoItem.Create("", "", null, new PriorityLevel(1));
            //Act
            var todo = new ToDoList(Title, Tagtype, new[] { todoItem });
            //Assert
            Assert.Equal(todo.Title, Title);
            Assert.Equal(todo.Tag, Tagtype);
            Assert.True(todo.ToDoItems.SequenceEqual(new[] { todoItem }));
        }

        [Fact]
        public void ToDoItem_Priority_Less_Than_One()
        {
            //Assert
            Assert.Throws<ValueIsOutOfRange>(() =>
            {
                var value = new PriorityLevel(0);
            });
        }
    }
}
