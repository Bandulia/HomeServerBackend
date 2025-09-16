using HomeServer.Core;
using HomeServer.Infrastructure;

namespace HomeServer.Application
{
    public static class TodoMapper
    {
        public static TodoEntity ToEntity(Todo todo)
        {
            return new TodoEntity
            {
                Title = todo.Title ?? string.Empty,
                Description = todo.Description,
                Priority = (Infrastructure.PriorityLevel)todo.Priority,
                IsRecurring = todo.IsRecurring,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt,
                StartedAt = todo.StartedAt,
                DueDate = todo.DueDate,
                CompletedAt = todo.CompletedAt,
                Notes = todo.GetNotes(),
                SubTasks = todo.GetSubTasks()
            };
        }

        public static Todo ToDomain(TodoEntity entity)
        {
            var todo = new Todo
            {
                Title = entity.Title,
                Description = entity.Description,
                Priority = (Core.PriorityLevel)entity.Priority
            };

            if (entity.DueDate.HasValue)
            {
                todo.DueDate = entity.DueDate;
            }

            todo.IsRecurring = entity.IsRecurring;
            todo.IsCompleted = entity.IsCompleted;

            foreach (var note in entity.Notes)
            {
                todo.AddNote(note);
            }

            foreach (var sub in entity.SubTasks)
            {
                todo.AddSubTask(sub.Key);
                if (sub.Value) todo.CompleteSubTask(sub.Key);
            }

            return todo;
        }
    }
}
