namespace SimpleSagaManager.UnitTests
{
    public class TaskSpy 
    {
        public bool IsCompleted() => StartIsCalled && !compensateIsCalled;
        public bool RooledBack() => compensateIsCalled;
        public bool StartIsCalled;
        protected bool compensateIsCalled;
    }
}