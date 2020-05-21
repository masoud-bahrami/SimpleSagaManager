using System;
using Xunit;

namespace SimpleSagaManager.UnitTests
{
    public class SimpleSagaManagerTests
    {
        private ITask<SharedDtoAcroosTasks > _okTask1 => new OkTask1();
        private ITask<SharedDtoAcroosTasks > _okTask2 => new OkTask2();
        private ITask<SharedDtoAcroosTasks > _bugyTask => new BugyTask();
        private ITask<SharedDtoAcroosTasks > _okTask4 => new OkTask4();


        [Fact]
        public void Should_Raise_ArgumentNullException_If_Starting_Task_Is_Null() {
            
            Assert.Throws<ArgumentNullException>(() => SimpleSagaManager<SharedDtoAcroosTasks >.StartWith(default));
        }

        [Fact]
        public void Should_Raise_ArgumentNullException_If_Any_Susequent_Tasks_Is_Null() {
            
            Assert.Throws<ArgumentNullException>(() => SimpleSagaManager<SharedDtoAcroosTasks >.StartWith(_okTask1).Then(default));
        }

        [Fact]
        public async void Should_Raise_ArgumentNullException_If_Context_Object_Is_Null() {
            var saga = SimpleSagaManager<SharedDtoAcroosTasks >.StartWith(_okTask1);

            await Assert.ThrowsAsync<ArgumentNullException>(() => saga.Run(default));
        }
        [Fact]
        public async void All_Tasks_Should_Be_Comppleted_If_There_Are_No_Any_Buggy_Task(){
            var notification = new Notification();
            var task1 = _okTask1;
            var task2 = _okTask2;
            var saga = SimpleSagaManager<SharedDtoAcroosTasks >.StartWith(task1).Then(task2);

            var result = await saga.Run(new Context<SharedDtoAcroosTasks > { Data = new SharedDtoAcroosTasks (), Notification = notification });

            Assert.True((task1 as OkTask1).IsCompleted());
            Assert.True((task2 as OkTask2).IsCompleted());
            Assert.False(result.Notification.HasError());
        }


        [Fact]
        public async void All_Previous_Tasks_Of_A_Buggy_Task_Must_Be_RooledBack_In_Reverse_Order(){
            var notification = new Notification();
            var task1 = _okTask1;
            var task2 = _okTask2;
            var buggyTask = _bugyTask;
            var task4 = _okTask4;
            var saga = SimpleSagaManager<SharedDtoAcroosTasks >.StartWith(task1).Then(task2).Then(buggyTask).Then(task4);

            var result = await saga.Run(new Context<SharedDtoAcroosTasks > { Data = new SharedDtoAcroosTasks (), Notification = notification });

            Assert.True((task1 as OkTask1).RooledBack());
            Assert.True((task2 as OkTask2).RooledBack());
            Assert.True((buggyTask as BugyTask).RooledBack());

            Assert.False((task4 as OkTask4).StartIsCalled);

            Assert.True(result.Notification.HasError());
        }
    }
}
