namespace RabbitMQ.Ports.App.RequestModel
{
    public class QueueRequest
    {
        public string Key { get; set; }

        public string Connection { get;  set; }

        public string Message { get; set; }

        public QueueRequest(string key, string connection, string message)
        {
            Key = key;
            Connection = connection;
            Message = message;
        }
    }
}
