using MashTodo.Models;
using MashTodo.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using TodoMashWPF.Helpers;
using TodoMashWPF.Models;

namespace TodoMashWPF.ViewModels
{
    public class AllTodosViewModel : INotifyPropertyChanged
    {
        private TodoItemService _service;

        private ObservableCollection<ObservableTodoItem> _todoItems;

        private bool _isLoading;

        private string _newTaskName = string.Empty;

        private string _statisticsLabel;

        private readonly Notifier _notifier;

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
                    NewTaskName = string.Empty;
                await RefreshTodos();
            }
            catch (ArgumentException ex)
            {
                _notifier.ShowInformation(ex.Message);
            }
            catch (TodoMashException ex)
            {
                _notifier.ShowError(ex.Message);
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
            catch (TodoMashException ex)
            {
                _notifier.ShowError(ex.Message);
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

        private ICommand _renameTaskCommand;

        public ICommand RenameTaskCommand
        {
            get
            {
                return _renameTaskCommand ?? (_renameTaskCommand = new DelegateCommand(OnRenameTask));
            }
        }

        private ICommand _deleteTaskCommand;

        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ?? (_deleteTaskCommand = new DelegateCommand(OnDeleteTask));
            }
        }

        private async void OnDeleteTask(object obj)
        {
            var todo = (TodoItem)obj;
            try
            {
                IsSending = true;
                await _service.Delete(todo.Id);
                await RefreshTodos();
            }
            catch (TodoMashException ex)
            {
                _notifier.ShowError(ex.Message);
            }
            finally
            {
                IsSending = false;
            }
        }

        private async void OnRenameTask(object obj)
        {
            var todo = (ObservableTodoItem)obj;
            bool nameChanged = todo.NewName != todo.Name;

            if (todo.IsCurrentlyEdited && nameChanged)
            {
                try
                {
                    IsSending = true;
                    todo.Name = todo.NewName;
                    await _service.Update(todo);
                    await RefreshTodos();
                    todo.IsCurrentlyEdited = false;
                }
                catch (TodoMashException ex)
                {
                    _notifier.ShowError(ex.Message);
                    todo.IsCurrentlyEdited = false;
                }
                finally
                {
                    IsSending = false;
                }
            }
            else if (todo.IsCurrentlyEdited && !nameChanged)
            {
                todo.IsCurrentlyEdited = false;
            }
            else
            {
                todo.IsCurrentlyEdited = true;
            }
        }

        public AllTodosViewModel(TodoItemService service)
        {
            IsLoading = true;
            _service = service;

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 40);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
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

        public ObservableCollection<ObservableTodoItem> TodoItems
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
                isSending = true;
                await RefreshTodos();
            }
            finally
            {
                isSending = false;
                IsLoading = false;
            }
        }

        public async Task RefreshTodos()
        {
            var todos = await _service.ReadAll();
            if (todos == null)
            {
                _notifier.ShowError("Server returned a null list of todos");
                return;
            }
            var observableTodoItems = todos.Select(x => new ObservableTodoItem(x)).OrderByDescending(x => x.ModifiedAt);
            TodoItems = new ObservableCollection<ObservableTodoItem>(observableTodoItems);
            StatisticsLabel = string.Format("Tasks added since app start: {0}", _service.GetTasksCreatedCount());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}