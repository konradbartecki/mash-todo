using MashTodo.Models;
using MashTodo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MashTodo.Service
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _Repository;
        private readonly StatisticsRepository _StatisticsRepository;
        private readonly MashAppConfig _MashAppConfig;

        public TodoItemService(ITodoItemRepository repository, StatisticsRepository statisticsRepository, MashAppConfig mashAppConfig)
        {
            _Repository = repository;
            _StatisticsRepository = statisticsRepository;
            _MashAppConfig = mashAppConfig;
        }

        public async Task<Guid> Create(string name)
        {
            bool isCorrectLength = name.Length >= _MashAppConfig.MiminumTaskNameLength && name.Length <= _MashAppConfig.MaximumTaskNameLength;
            if (!isCorrectLength)
            {
                throw new ArgumentException($"Name of the task is either too short or too long. Actual value = {name.Length}, Expected value has to be be between 3 and 50");
            }

            var item = new TodoItem
            {
                Id = Guid.NewGuid(),
                ModifiedAt = DateTimeOffset.Now,
                Name = name,
                Status = TodoStatus.Open
            };

            await _Repository.Create(item);
            _StatisticsRepository.RaiseCreatedCount();
            return item.Id;
        }

        public async Task<TodoItem> Find(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return (await _Repository.ReadAll()).FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(TodoItem item)
        {
            item.ModifiedAt = DateTimeOffset.Now;
            await _Repository.Update(item);
        }

        public async Task<IEnumerable<TodoItem>> ReadAll()
        {
            return await _Repository.ReadAll();
        }

        public async Task Delete(Guid id)
        {
            await _Repository.Delete(id);
        }

        public int GetTasksCreatedCount()
        {
            return _StatisticsRepository.AllTodosCreatedCount;
        }
    }
}