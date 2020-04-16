using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using server;

public class TcpTimeServer
{
    private const int portNum = 22740;
    public List<LiveClient> LiveClients { get; set; }
    public static int Main(String[] args)
    {
        Console.Write("Server started");
        var listener = new TcpListener(IPAddress.Any, portNum);
        listener.Start();

        var liveClients = new List<LiveClient>();
        while (true)
        {
            //accepts new clientRequests, on timer
            if (listener.Pending())
            {
                TcpClient client = listener.AcceptTcpClient();

                var id = client.Client.RemoteEndPoint.ToString().Split(':')[1];
                Console.WriteLine("Connection accepted w/ endpoint id " + id);
                NetworkStream ns = client.GetStream();
            
                // add newly aquired connections to our liveClients
                liveClients.Add(new LiveClient
                {
                    Id = id,
                    NetStream = ns
                });
            
                // send liveClients to our client. Send it to client(s)? 
                var sb =  new StringBuilder();
                foreach (var liveClient in liveClients)
                {
                    sb.Append(liveClient.Id);
                    sb.Append(" ");
                }
                byte[] listOfAvailableClients = Encoding.ASCII.GetBytes("available clients: " + sb.ToString());
                ns.Write(listOfAvailableClients, 0, listOfAvailableClients.Length);
            }
            

            // var noNewData = true;
            // while (noNewData)
            // {
            //     foreach (var liveClient in liveClients)
            //     {
            //         if (liveClient.NetStream.DataAvailable)
            //         {
            //             noNewData = false;
            //         }
            //     }
            // }
            
            //read input from clients
            if (liveClients.Count > 1)
            {
                foreach (var liveClient in liveClients)
                {
                    if (liveClient.NetStream.DataAvailable)
                    {
                        byte[] bytesFromMsg = new byte[1024];
                        int bytesReadFromMsg = liveClient.NetStream.Read(bytesFromMsg, 0, bytesFromMsg.Length);
                        InterpretMsg(Encoding.ASCII.GetString(bytesFromMsg, 0, bytesReadFromMsg), liveClients);
                    }
                }
            }
        }
    }

    // great place for error checking incoming msgs
    public static void InterpretMsg(string idAndMsg, List<LiveClient> liveClients)
    {
        var idAndMsgTuple = idAndMsg.Split(' ', 2);
        var id = idAndMsgTuple[0];
        var msg = idAndMsgTuple[1];
        
        foreach (var liveClient in liveClients)
        {
            if (liveClient.Id.Contains(id))
            {
                WriteToClient(msg, liveClient);
            }
        }
    }
    public static void WriteToClient(string msg, LiveClient targetClient )
    {
        
        byte[] msgBytes = Encoding.ASCII.GetBytes(msg);
        targetClient.NetStream.Write(msgBytes, 0, msgBytes.Length);
    }

    public string ReadFromClient()
    {
        return "";
    }
}

/* todo:
 * connections terminated from the tcp side arent cleared out of liveClients
 * 
 */