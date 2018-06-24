using MashTodo.Models;
using MashTodo.Repository;
using MashTodo.Service;
using Moq;
using NUnit.Framework;
using System;

namespace TodoMashWPF.UnitTests
{
    [TestFixture]
    public class TodoItemServiceTests
    {

        [Test]
        public void CanCreateTask()
        {
            //arrange
            string taskName = "new Test Task";

            var settingsMock = new Mock<IMashAppConfig>();
            settingsMock.Setup(x => x.MiminumTaskNameLength).Returns(3);
            settingsMock.Setup(x => x.MaximumTaskNameLength).Returns(50);

            var repoMock = new Mock<ITodoItemRepository>();


            var statsMock = new Mock<IStatisticsRepository>();


            var service = new TodoItemService(repoMock.Object, statsMock.Object, settingsMock.Object);

            //act
            var createdGuid = service.Create(taskName).Result;            

            //assert
            repoMock.Verify(x =>
                x.Create(It.Is<TodoItem>(y => 
                    y.Name == taskName 
                    && y.Status == TodoStatus.Open
                    && y.Id == createdGuid)),
                Times.Once);
            statsMock.Verify(x => x.RaiseCreatedCount(), Times.Once);
        }

        [Test]
        public void ThrowsTaskNameIsTooShort()
        {
            //arrange
            string taskName = "n";

            var settingsMock = new Mock<IMashAppConfig>();
            settingsMock.Setup(x => x.MiminumTaskNameLength).Returns(3);
            settingsMock.Setup(x => x.MaximumTaskNameLength).Returns(50);

            var repoMock = new Mock<ITodoItemRepository>();


            var statsMock = new Mock<IStatisticsRepository>();


            var service = new TodoItemService(repoMock.Object, statsMock.Object, settingsMock.Object);

            //act & assert
            Assert.That(() => service.Create(taskName).Wait(),
                Throws.Exception
                .TypeOf<AggregateException>()
                .With.Property("InnerException").TypeOf<ArgumentException>());
        }

        [Test]
        public void ThrowsTaskNameIsTooLong()
        {
            //arrange
            string taskName = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest";

            var settingsMock = new Mock<IMashAppConfig>();
            settingsMock.Setup(x => x.MiminumTaskNameLength).Returns(3);
            settingsMock.Setup(x => x.MaximumTaskNameLength).Returns(50);

            var repoMock = new Mock<ITodoItemRepository>();


            var statsMock = new Mock<IStatisticsRepository>();


            var service = new TodoItemService(repoMock.Object, statsMock.Object, settingsMock.Object);

            //act & assert
            Assert.That(() => service.Create(taskName).Wait(),
                Throws.Exception
                .TypeOf<AggregateException>()
                .With.Property("InnerException").TypeOf<ArgumentException>());
        }

        [Test]
        public void ThrowsTaskNameIsNull()
        {
            string taskName = null;

            var settingsMock = new Mock<IMashAppConfig>();
            settingsMock.Setup(x => x.MiminumTaskNameLength).Returns(3);
            settingsMock.Setup(x => x.MaximumTaskNameLength).Returns(50);
            var repoMock = new Mock<ITodoItemRepository>();
            var statsMock = new Mock<IStatisticsRepository>();
            var service = new TodoItemService(repoMock.Object, statsMock.Object, settingsMock.Object);

            //act
            Assert.That(() => service.Create(taskName).Wait(),
                Throws.Exception
                .TypeOf<AggregateException>()
                .With.Property("InnerException").TypeOf<ArgumentException>());
        }
    }
}
