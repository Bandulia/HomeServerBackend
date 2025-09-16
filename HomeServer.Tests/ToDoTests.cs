namespace HomeServer.Tests
{
    public class TodoTests
    {
        private Core.Todo CreateToDo()
        {
            return new Core.Todo
            {
                Title = "Test Task",
                Description = "This is a test task",
            };
        }

        [Fact]
        public void Constructor_TitleAndDescription_AreSetCorrectly()
        {
            var _todo = CreateToDo();
            Assert.Equal("Test Task", _todo.Title);
            Assert.Equal("This is a test task", _todo.Description);
        }

        [Fact]
        public void Description_IsSetCorrectly()
        {
            var _todo = CreateToDo();
            Assert.Equal(Core.PriorityLevel.Low, _todo.Priority);
            Assert.False(_todo.IsCompleted);
            Assert.False(_todo.IsRecurring);
            Assert.Null(_todo.DueDate);
            Assert.Null(_todo.StartedAt);
            Assert.Null(_todo.CompletedAt);
        }

        [Fact]
        public void DueDate_CanBeSetAndCleared()
        {
            var _todo = CreateToDo();
            var dueDate = DateTime.Now.AddDays(5);
            _todo.DueDate = dueDate;
            Assert.Equal(dueDate, _todo.DueDate);
            _todo.DueDate = null;
            Assert.Null(_todo.DueDate);
        }

        [Fact]
        public void SetPriority_UpdatesValueCorrectly()
        {
            var _todo = CreateToDo();
            _todo.Priority = Core.PriorityLevel.High;
            Assert.Equal(Core.PriorityLevel.High, _todo.Priority);
            _todo.Priority = Core.PriorityLevel.Medium;
            Assert.Equal(Core.PriorityLevel.Medium, _todo.Priority);
        }

        [Fact]
        public void Recurrence_CanBeToggled()
        {
            var _todo = CreateToDo();
            Assert.False(_todo.IsRecurring);
            _todo.IsRecurring = true;
            Assert.True(_todo.IsRecurring);
            _todo.IsRecurring = false;
            Assert.False(_todo.IsRecurring);
        }

        [Fact]
        public void AddNote_ThenRemoveNote_NotesListUpdatedCorrectly()
        {
            var _todo = CreateToDo();
            _todo.AddNote("First note");
            Assert.Contains("First note", _todo.GetNotes());
            _todo.RemoveNote("First note");
            Assert.DoesNotContain("First note", _todo.GetNotes());
        }

        [Fact]
        public void SubTasks_AreCompletedCorrectly()
        {
            var _todo = CreateToDo();
            _todo.AddSubTask("Subtask 1");
            _todo.AddSubTask("Subtask 2");
            Assert.Contains("Subtask 1", _todo.GetSubTasks().Keys);
            Assert.Contains("Subtask 2", _todo.GetSubTasks().Keys);
            Assert.False(_todo.CompleteSubTask("Subtask 1"));
            Assert.Null(_todo.CompletedAt);
            Assert.True(_todo.CompleteSubTask("Subtask 2"));
            Assert.NotNull(_todo.CompletedAt);
            Assert.True(_todo.IsCompleted);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Completion_StatusChangesCorrectly(bool isCompleted)
        {
            var _todo = CreateToDo();
            _todo.IsCompleted = isCompleted;
            if (isCompleted)
            {
                Assert.NotNull(_todo.CompletedAt);
                Assert.True(_todo.IsCompleted);
            }
            else
            {
                Assert.Null(_todo.CompletedAt);
                Assert.False(_todo.IsCompleted);
            }
        }

        [Fact]
        public void Completed_StatusChangesCorrectly()
        {
            var _todo = CreateToDo();
            _todo.AddSubTask("Subtask 1");
            _todo.AddSubTask("Subtask 2");
            _todo.CompleteSubTask("Subtask 1");
            Assert.False(_todo.IsCompleted);
            Assert.Null(_todo.CompletedAt);
            _todo.CompleteSubTask("Subtask 2");
            Assert.True(_todo.IsCompleted);
            Assert.NotNull(_todo.CompletedAt);
            Assert.True(_todo.IsCompleted);
        }

        [Fact]
        public void DueDate_CannotBeSetBeforeCreatedAt()
        {
            var _todo = CreateToDo();
            var invalidDate = _todo.CreatedAt.AddMinutes(-1);
            var ex = Assert.Throws<ArgumentException>(() => _todo.DueDate = invalidDate);
            Assert.Equal("Due date cannot be earlier than creation date.", ex.Message);
        }

        [Fact]
        public void SubTask_CanBeUnmarkedAsCompleted()
        {
            var _todo = CreateToDo();
            _todo.AddSubTask("Task1");
            _todo.CompleteSubTask("Task1");
            Assert.True(_todo.GetSubTasks()["Task1"]);

            _todo.UnmarkSubtaskAsCompleted("Task1");
            Assert.False(_todo.GetSubTasks()["Task1"]);
            Assert.False(_todo.IsCompleted);
        }

        [Fact]
        public void UpdatedAt_IsChangedWhenNoteAdded()
        {
            var _todo = CreateToDo();
            var before = _todo.UpdatedAt;
            Thread.Sleep(10);

            _todo.AddNote("Note");
            Assert.True(_todo.UpdatedAt > before);
        }

        [Fact]
        public void AddSubTask_AddDuplicated_DoesNothing()
        {
            var _todo = CreateToDo();
            _todo.AddSubTask("Subtask 1");
            _todo.AddSubTask("Subtask 1");
            Assert.Single(_todo.GetSubTasks());
        }

        [Fact]
        public void RemoveNote_DoesNotThrowIfNoteNotFound()
        {
            var _todo = CreateToDo();
            _todo.RemoveNote("Nonexistent note");
            Assert.Empty(_todo.GetNotes());
        }

        [Fact]
        public void UpdatedAt_IsChangedWhenDueDateIsSet()
        {
            var _todo = CreateToDo();
            var before = _todo.UpdatedAt;
            Thread.Sleep(10);
            _todo.DueDate = DateTime.Now.AddDays(1);
            Assert.True(_todo.UpdatedAt > before);
        }


        [Fact]
        public void GetNotes_ReturnsCopy()
        {
            var _todo = CreateToDo();
            var notes = _todo.GetNotes();
            notes.Add("Extern note");
            Assert.DoesNotContain("Extern note", _todo.GetNotes());
        }
    }
}
