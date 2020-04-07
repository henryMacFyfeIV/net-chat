using System.Net.Sockets;

namespace server
{
    public class LiveClient
    {
        public string id { get; set; }
        public TcpClient tcpClient { get; set; }
    }
}