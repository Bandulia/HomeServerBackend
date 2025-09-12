namespace HomeServer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ToDoTest()
        {
            // Arrange & Act
            var todo = new Core.ToDo
            {
                Title = "Test Task",
                Description = "This is a test task",
                IsRecurring = false
            };
            Assert.Equal("Test Task", todo.Title);
            Assert.Equal("This is a test task", todo.Description);
            Assert.Equal(Core.PriorityLevel.Low, todo.Priority);
            Assert.False(todo.IsCompleted);
            Assert.False(todo.IsRecurring);
            Assert.NotNull(todo.CreatedAt);
            Assert.Null(todo.DueDate);
            Assert.Null(todo.StartedAt);
            Assert.Null(todo.CompletedAt);

            // Notes
            todo.AddNote("First note");
            Assert.Contains("First note", todo.GetNotes());
            todo.RemoveNote("First note");
            Assert.DoesNotContain("First note", todo.GetNotes());

            // Recurring
            todo.IsRecurring = true;
            Assert.True(todo.IsRecurring);
            Assert.NotNull(todo.StartedAt);
            todo.IsRecurring = false;
            Assert.False(todo.IsRecurring);
            Assert.Null(todo.StartedAt);
               
            todo.AddSubTask("Subtask 1");
            todo.AddSubTask("Subtask 2");
            Assert.Contains("Subtask 1", todo.GetSubTasks().Keys);
            Assert.Contains("Subtask 2", todo.GetSubTasks().Keys);
            todo.CompleteSubTask("Subtask 1");
            Assert.Null(todo.CompletedAt);
            todo.CompleteSubTask("Subtask 2");
            Assert.NotNull(todo.CompletedAt);

            // Priority
            todo.Priority = Core.PriorityLevel.Critical;
            Assert.Equal(Core.PriorityLevel.Critical, todo.Priority); 


        }
    }
}
