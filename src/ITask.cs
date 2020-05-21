using System.Threading.Tasks;

namespace SimpleSagaManager
{
    public interface ITask<T>
    {
        Task<Context<T>> StartAsync(Context<T> context);
        Task<Context<T>> CompensateAsync(Context<T> context);
    }
}