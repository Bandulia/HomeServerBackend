using HomeServer.Core;
using HomeServer.Infrastructure;


namespace HomeServer.Application
{
    internal class EfTodoRepository : ITodoRepository
    {
        private readonly TodoListDbContext _db;

        public EfTodoRepository(TodoListDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Todo todo, CancellationToken ct = default)
        {
            var entity = TodoMapper.ToEntity(todo);
            _db.Todos.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.Todos.FindAsync(new object[] { id }, ct);
            if (entity != null)
            {
                _db.Todos.Remove(entity);
                await _db.SaveChangesAsync(ct);
            }
        }

        public async Task<Todo?> GetAsync(int id, CancellationToken ct = default)
                => (await _db.Todos.FindAsync([id], ct)) is { } e ? TodoMapper.ToDomain(e) : null;

        public async Task UpdateAsync(Todo todo, CancellationToken ct = default)
        {
           throw new NotImplementedException();
        }
    }
}
