namespace MashTodo.Models
{
    /// <summary>
    /// Status is an enum instead of bool, so I can add "InProgress" status or anything else later
    /// </summary>
    public enum TodoStatus
    {
        Unknown = 0,
        Open = 1,
        Completed = 2
    }
}