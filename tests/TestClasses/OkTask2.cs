using System.Threading.Tasks;

namespace SimpleSagaManager.UnitTests
{
    public class OkTask2 : TaskSpy, ITask<SharedDtoAcroosTasks >
    {
        public async Task<Context<SharedDtoAcroosTasks >> StartAsync(Context<SharedDtoAcroosTasks > context) {
            StartIsCalled = true;
            return context;
        }
        public async Task<Context<SharedDtoAcroosTasks >> CompensateAsync(Context<SharedDtoAcroosTasks > context) {
            compensateIsCalled = true;
            return context;
        }
    }
}
