namespace MashTodo.Repository
{
    public interface IStatisticsRepository
    {
        int AllTodosCreatedCount { get; }

        void RaiseCreatedCount();
    }
}