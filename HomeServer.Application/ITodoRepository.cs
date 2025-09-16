using HomeServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServer.Application
{
    internal interface ITodoRepository
    {
        Task<Todo?> GetAsync(int id, CancellationToken ct = default);
        Task<int> AddAsync(Todo todo, CancellationToken ct = default);
        Task UpdateAsync(Todo todo, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
