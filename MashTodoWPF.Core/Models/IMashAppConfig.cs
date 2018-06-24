namespace MashTodo.Service
{
    public interface IMashAppConfig
    {
        string BaseAddress { get; }
        int MaximumTaskNameLength { get; }
        int MiminumTaskNameLength { get; }
    }
}