using MashTodo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MashTodo.Repository
{
    public interface ITodoItemRepository
    {
        Task<Guid> Create(TodoItem item);

        Task Delete(Guid guid);

        Task<IEnumerable<TodoItem>> ReadAll();

        Task Update(TodoItem item);
    }
}