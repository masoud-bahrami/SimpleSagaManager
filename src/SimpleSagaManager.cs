using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exception = System.Exception;

namespace SimpleSagaManager
{
    public class SimpleSagaManager<T>
    {
        private readonly Queue<ITask<T>> _tasks;
        private SimpleSagaManager(ITask<T> task) {

            GaurdAgainstNullObject(task, "task");
            _tasks = new Queue<ITask<T>>();
            _tasks.Enqueue(task);
        }
        public static SimpleSagaManager<T> StartWith(ITask<T> task) {
            return new SimpleSagaManager<T>(task);
        }

        public SimpleSagaManager<T> Then(ITask<T> task) {
            GaurdAgainstNullObject(task, "task");

            _tasks.Enqueue(task);
            return this;
        }

        public void Setup() {
            GaurdAgainstNullOrEmptyCollection(_tasks, "Tasks");
        }

        public async Task<Context<T>> Run(Context<T> context) {

            GaurdAgainstNullObject(context , "cootext");

            Stack<ITask<T>> stack = new Stack<ITask<T>>();

            var iterator = _tasks.GetEnumerator();
            while (iterator.MoveNext())
            {
                var currentTask = iterator.Current;
                stack.Push(currentTask);

                try
                {
                    context = await currentTask.StartAsync(context);

                    if (context.Notification.HasError())
                        throw new Exception();
                }
                catch (Exception)
                {
                    var iterator1 = stack.GetEnumerator();
                    while (iterator1.MoveNext())
                    {
                        var task = iterator1.Current;
                        context = await task.CompensateAsync(context);
                    }
                    break;
                }
            }

            return context;
        }

        private void GaurdAgainstNullObject(object obj, string name) {
            if (obj == null)
                throw new ArgumentNullException(name);
        }
        private void GaurdAgainstNullOrEmptyCollection(ICollection collection, string name) {
            if (collection == null || collection.Count == 0)
                throw new ArgumentNullException(name);
        }
    }
}