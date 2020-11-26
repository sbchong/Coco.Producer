using System;
using System.Net;
using System.Net.Sockets;

namespace Coco.Producer
{
    public class Producer : IProducer
    {
        public string Host { get; set; } = "127.0.0.1";
        public Producer(string hostUrl)
        {
            Host = hostUrl;
        }
        public void Push(string host, string topicName, string message)
        {
            IPAddress ipa = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ipa, 9527);
            Send(ipe, topicName, message);
        }
        public void Push(string topicName, string message)
        {
            IPAddress ipa = IPAddress.Parse(Host);
            IPEndPoint ipe = new IPEndPoint(ipa, 9527);
            Send(ipe, topicName, message);
        }

        public void Send(IPEndPoint ipe, string topicName, string message)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ipe);
                if (tcpClient.Connected)
                {
                    CommunicationBase cb = new CommunicationBase();
                    var content = $"0\\{topicName}\\{message}";
                    cb.SendMsg(content, tcpClient);
                    cb.ReceiveMsg(tcpClient);
                }
            }
            catch (Exception ex)
            {
                tcpClient.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
