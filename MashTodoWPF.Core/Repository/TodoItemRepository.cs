using MashTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MashTodo.Repository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoMashDbContext _DbContext;

        public TodoItemRepository(TodoMashDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItem>> ReadAll()
        {
            return await _DbContext.TodoItem.ToAsyncEnumerable().ToList();
        }

        public async Task<Guid> Create(TodoItem item)
        {
            _DbContext.TodoItem.Add(item);
            await _DbContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task Update(TodoItem item)
        {
            _DbContext.TodoItem.Update(item);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid guid)
        {
            var itemToRemove = await _DbContext.TodoItem.FindAsync(guid);

            if (itemToRemove == null)
            {
                throw new ArgumentException($"Unable to find a todo item with id {guid}");
            }

            _DbContext.TodoItem.Remove(itemToRemove);
            await _DbContext.SaveChangesAsync();
        }
    }
}