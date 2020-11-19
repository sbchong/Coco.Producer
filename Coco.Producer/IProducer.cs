namespace Coco.Producer
{
    public interface IProducer
    {
        void Push(string topicName, string message);
        void Push(string host, string topicName, string message);
    }
}