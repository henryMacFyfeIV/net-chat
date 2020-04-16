using System.Net.Sockets;

namespace server
{
    public class LiveClient
    {
        public string Id { get; set; }
        public NetworkStream NetStream { get; set; }
    }
}