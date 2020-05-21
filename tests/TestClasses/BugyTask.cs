using System;
using System.Threading.Tasks;

namespace SimpleSagaManager.UnitTests
{
    public class BugyTask : TaskSpy, ITask<SharedDtoAcroosTasks >
    {
        public async Task<Context<SharedDtoAcroosTasks >> StartAsync(Context<SharedDtoAcroosTasks > context) {
            StartIsCalled = true;
            context.Notification.AddError(new Exception());
            return context;
        }
        public async Task<Context<SharedDtoAcroosTasks >> CompensateAsync(Context<SharedDtoAcroosTasks > context) {
            compensateIsCalled = true;
            return context;
        }
    }
}
