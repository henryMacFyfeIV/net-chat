using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TcpTimeServer
{

    private const int portNum = 13;

    public static int Main(String[] args)
    {
        bool done = false;

        var listener = new TcpListener(IPAddress.Any, portNum);

        listener.Start();

        while (true)
        {
            Console.Write("Waiting for connection...");
            TcpClient client = listener.AcceptTcpClient();
            
            Console.WriteLine("Connection accepted w/ endpoint id " + client.Client.RemoteEndPoint.ToString());
            NetworkStream ns = client.GetStream();
        
            byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

            try
            {
                ns.Write(byteTime, 0, byteTime.Length);
                // ns.Close();
                // client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            byte[] bytes = new byte[1024];
            int bytesRead = ns.Read(bytes, 0, bytes.Length);
            
            Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));
        }

        listener.Stop();

        return 0;
    }

}