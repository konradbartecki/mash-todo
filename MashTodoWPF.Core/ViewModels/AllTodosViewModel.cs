using MashTodo.Models;
using MashTodo.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoMashWPF.Helpers;

namespace TodoMashWPF.ViewModels
{
    public class AllTodosViewModel : INotifyPropertyChanged
    {
        private TodoItemService _service;

        private ObservableCollection<TodoItem> _todoItems;

        private bool _isLoading;

        private string _newTaskName;

        private string _statisticsLabel;

        public string StatisticsLabel
        {
            get { return _statisticsLabel; }
            set
            {
                _statisticsLabel = value;
                NotifyPropertyChanged();
            }
        }

        public string NewTaskName
        {
            get { return _newTaskName; }
            set
            {
                _newTaskName = value;
                NotifyPropertyChanged();
            }
        }

        private async void AddNewTask(object sender)
        {
            try
            {
                IsSending = true;
                var guid = await _service.Create(NewTaskName);
                if (guid != Guid.Empty)
                    NewTaskName = null;
                await RefreshTodos();
            }
            catch (ArgumentException)
            {
                //show blue alert
            }
            catch (TodoMashException)
            {
                //show red alert
            }
            finally
            {
                IsSending = false;
            }
        }

        private async void OnTaskStatusChange(object sender)
        {
            try
            {
                IsSending = true;
                await _service.Update((TodoItem)sender);
                await RefreshTodos();
            }
            finally
            {
                IsSending = false;
            }
        }

        private ICommand _addTaskCommand;

        public ICommand AddTaskCommand
        {
            get
            {
                return _addTaskCommand ?? (_addTaskCommand = new DelegateCommand(AddNewTask));
            }
        }

        private ICommand _taskStatusChangeCommand;

        public ICommand TaskStatusChangeCommand
        {
            get
            {
                return _taskStatusChangeCommand ?? (_taskStatusChangeCommand = new DelegateCommand(OnTaskStatusChange));
            }
        }

        public AllTodosViewModel(TodoItemService service)
        {
            IsLoading = true;
            _service = service;
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                NotifyPropertyChanged();
            }
        }

        private bool isSending;

        public bool IsSending
        {
            get { return isSending; }
            set
            {
                isSending = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set
            {
                _todoItems = value;
                NotifyPropertyChanged();
            }
        }

        public async Task Initialize()
        {
            try
            {
                IsLoading = true;
                await RefreshTodos();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task RefreshTodos()
        {
            var todos = await _service.ReadAll();
            TodoItems = new ObservableCollection<TodoItem>(todos?.OrderByDescending(x => x.ModifiedAt));
            StatisticsLabel = string.Format("Tasks added since app start: {0}", _service.GetTasksCreatedCount());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}