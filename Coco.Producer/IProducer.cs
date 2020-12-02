namespace Coco.Producer
{
    public interface IProducer
    {
        public string Host { get; set; }
        public string Port { get; set; }

        void Publish(string topicName, string message);
        void Publish(string hostUrl, string topicName, string message);
    }
}