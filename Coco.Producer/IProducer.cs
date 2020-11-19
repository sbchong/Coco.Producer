namespace Coco.Producer
{
    public interface IProducer
    {
        void Push(string topicName, string msg);
        void Push(string host, string topicName, string msg);
    }
}