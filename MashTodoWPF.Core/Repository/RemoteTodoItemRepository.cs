using MashTodo.Models;
using MashTodo.Repository;
using MashTodo.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoMashWPF.Repositories
{
    /// <summary>
    /// For use in thin clients like WPF or UWP app that are going to send an item instead of saving it to the database
    /// </summary>
    public class RemoteTodoItemRepository : ITodoItemRepository
    {
        private readonly RestClientService _RestClient;

        public RemoteTodoItemRepository(RestClientService restClient)
        {
            _RestClient = restClient;
        }

        public async Task<Guid> Create(TodoItem item)
        {
            return await _RestClient.CreateTask(item);
        }

        public async Task Delete(Guid guid)
        {
            await _RestClient.DeleteTask(guid);
        }

        public async Task<IEnumerable<TodoItem>> ReadAll()
        {
            return await _RestClient.GetAllTodos();
        }

        public async Task Update(TodoItem item)
        {
            await _RestClient.UpdateTask(item);
        }
    }
}