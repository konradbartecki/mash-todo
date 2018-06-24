using System;

namespace MashTodo.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public StatisticsRepository()
        {
        }

        public StatisticsRepository(int createdCount)
        {
            if (createdCount > 0)
            {
                _allTodosCreatedCount = createdCount;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(createdCount), createdCount,
                    $"Creation of {nameof(StatisticsRepository)} was attempted with {nameof(createdCount)} value of {createdCount}, but expected value should be always higher than 0.");
            }
        }

        private int _allTodosCreatedCount = 0;

        public int AllTodosCreatedCount
        {
            get { return _allTodosCreatedCount; }
        }

        public void RaiseCreatedCount()
        {
            _allTodosCreatedCount++;
        }
    }
}