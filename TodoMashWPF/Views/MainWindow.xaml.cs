using System.Windows;
using TodoMashWPF.ViewModels;

namespace TodoMashWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AllTodosViewModel;
            await vm.Initialize();
        }
    }
}