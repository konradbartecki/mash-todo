using System;

namespace MashTodo.Models
{
    public class TodoMashException : Exception
    {
        public TodoMashException()
        {
        }

        public TodoMashException(string message) : base(message)
        {
        }
    }
}