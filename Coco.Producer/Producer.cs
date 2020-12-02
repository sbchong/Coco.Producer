using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace Coco.Producer
{
    public class Producer : IProducer
    {
        public string Host { get; set; } = "127.0.0.1";
        public string Port { get; set; } = "9527";
        public Producer()
        {

        }
        public Producer(string host,string port)
        {
            Host = host;
            Port = port;
        }
        public void Publish(string hostUrl, string topicName, string message)
        {
            string[] hp = hostUrl.Split(':');
            IPAddress ipa = IPAddress.Parse(hp[0]);
            IPEndPoint ipe = new IPEndPoint(ipa, Convert.ToInt32(hp[1]));
            Send(ipe, topicName, message);
        }
        public void Publish(string topicName, string message)
        {
            IPAddress ipa = IPAddress.Parse(Host);
            IPEndPoint ipe = new IPEndPoint(ipa, Convert.ToInt32(Port));
            Send(ipe, topicName, message);
        }
        public void Publish(string topicName, object msg)
        {
            IPAddress ipa = IPAddress.Parse(Host);
            IPEndPoint ipe = new IPEndPoint(ipa, Convert.ToInt32(Port));
            string json = JsonSerializer.Serialize(msg, new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Send(ipe, topicName, json);
        }
        public void Publish<T>(string topicName, T msg) where T : class
        {
            IPAddress ipa = IPAddress.Parse(Host);
            IPEndPoint ipe = new IPEndPoint(ipa, Convert.ToInt32(Port));
            string json = JsonSerializer.Serialize(msg, new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Send(ipe, topicName, json);
        }

        private void Send(IPEndPoint ipe, string topicName, string message)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ipe);
                if (tcpClient.Connected)
                {
                    CommunicationBase cb = new CommunicationBase();
                    var content = $"0^^^{topicName}^^^{message}";
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
