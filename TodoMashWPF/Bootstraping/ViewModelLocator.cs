using Autofac;
using MashTodo.Repository;
using MashTodo.Service;
using TodoMashWPF.Repositories;
using TodoMashWPF.ViewModels;

namespace MashTodoWPF.Core.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IContainer container;

        public ViewModelLocator()
        {
            var builder = GetContainerBuilder();
            this.container = builder.Build();
        }

        public virtual ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AllTodosViewModel>();
            containerBuilder.RegisterType<MashAppConfig>().As<IMashAppConfig>();
            containerBuilder.RegisterType<StatisticsRepository>().As<IStatisticsRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TodoItemService>();
            containerBuilder.RegisterType<RestClientService>();
            containerBuilder.RegisterType<RemoteTodoItemRepository>().As<ITodoItemRepository>();

            return containerBuilder;
        }

        public AllTodosViewModel AllTodosViewModel => container.Resolve<AllTodosViewModel>();
    }
}