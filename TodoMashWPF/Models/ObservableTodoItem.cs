using MashTodo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TodoMashWPF.Models
{
    public class ObservableTodoItem : TodoItem, INotifyPropertyChanged
    {
        public ObservableTodoItem()
        {
        }

        public ObservableTodoItem(TodoItem item)
        {
            this.Id = item.Id;
            this.ModifiedAt = item.ModifiedAt;
            this.Name = item.Name;
            this.NewName = item.Name;
            this.Status = item.Status;
        }

        private bool _isCurrentlyEdited;

        public bool IsCurrentlyEdited
        {
            get { return _isCurrentlyEdited; }
            set
            {
                _isCurrentlyEdited = value;
                NotifyPropertyChanged();
            }
        }


        public string NewName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
