using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MashTodo.Models
{
    public class TodoItem
    {
        public TodoItem()
        {

        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public TodoStatus Status { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }
    }
}
