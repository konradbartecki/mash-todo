using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
