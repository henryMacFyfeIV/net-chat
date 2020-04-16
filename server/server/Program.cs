using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using server;

public class TcpTimeServer
{

    private const int portNum = 22740;

    public static int Main(String[] args)
    {
        bool done = false;
        var listener = new TcpListener(IPAddress.Any, portNum);
        var WaitingRoom = new List<LiveClient>();

        listener.Start();

        /* happy path 1:
        *  server launches
        *  server accepts client1 connection, adds them to a "Waiting room" collection, sends client1 a msg
        *  server accepts client2 connection, adds them to a "Waiting room" collection, sends client1 a msg
        *  client1 sends a "get available clients" (probably 'ls') query that returns all clients in waiting room
        *  client1 sends a "connection request for client2"
        *  client1, client2 create "client2client" object chat1
        *  chat1 is added to "active chats"
         * client1 and client2 are removed from Waiting room"
        *  server sends a msg to client1 and client2 that they are connected to each other
        */
    
        /*
         * happy path 2: emergency reductions
         * server launches
         * client 1 launches 
         * 
         */
        var liveClients = new List<string>();
        while (true)
        {
            Console.Write("Server started");
            TcpClient client = listener.AcceptTcpClient();
            
            Console.WriteLine("Connection accepted w/ endpoint id " + client.Client.RemoteEndPoint.ToString());
            var id = client.Client.RemoteEndPoint.ToString();
            NetworkStream ns = client.GetStream();
            
            byte[] listOfAvailableClients = Encoding.ASCII.GetBytes("clientTest");
            ns.Write(listOfAvailableClients, 0, listOfAvailableClients.Length);
            
            // WaitingRoom.Add(item: new LiveClient()
            // {
            //     id = id.Substring(id.LastIndexOf('.')),
            //     tcpClient = client
            // });
            
            
            // byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            // try
            // { 
            //   
            //     ns.Write(byteTime, 0, byteTime.Length);
            //     // ns.Close();
            //     // client.Close();
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.ToString());
            // }

            byte[] bytes = new byte[1024];
            int bytesRead = ns.Read(bytes, 0, bytes.Length);
            
            Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));
        }
    }

    // public void WriteToClient(string msg, )
    // {
    //     
    // }

}