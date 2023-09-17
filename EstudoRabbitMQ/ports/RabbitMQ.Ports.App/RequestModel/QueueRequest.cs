namespace RabbitMQ.Ports.App.RequestModel
{
    public class QueueRequest
    {
        public string Key { get; set; }

        public string Connection { get;  set; }

        public List<TodoRequest> Todo { get; set; }

        public QueueRequest(string key, string connection, List<TodoRequest> todo)
        {
            Key = key;
            Connection = connection;
            Todo = todo;
        }
    }

    public class TodoRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TodoRequest(string title, string description) 
        {
            Title = title;
            Description = description;
        }
    }
}
