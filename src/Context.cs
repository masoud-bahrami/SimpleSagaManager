namespace SimpleSagaManager
{
    public class Context<T>
    {
        public T Data { get; set; }
        public Notification Notification { get; set; } = new Notification();
        public static Context<T> New(T data , Notification notification)
        {
            return new Context<T>
            {
                Data = data,
                Notification = notification
            };
        }
    }
}