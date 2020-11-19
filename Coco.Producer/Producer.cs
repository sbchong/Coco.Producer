using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
            //string Host = "127.0.0.1";
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
                    cb.SendMsg(0.ToString(), tcpClient);
                    cb.ReceiveMsg(tcpClient);
                    cb.SendMsg(topicName, tcpClient);
                    cb.ReceiveMsg(tcpClient);
                    //Console.WriteLine("生产者推送消息：{0}", msg);
                    cb.SendMsg(message, tcpClient);
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
