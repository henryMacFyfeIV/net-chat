using System;
using System.Net.Sockets;
using System.Text;

public class TcpTimeClient
{
    private const int portNum = 13;
    private const string hostName = "127.0.0.1";

    public static int Main(String[] args)
    {
        try
        {
            var client = new TcpClient(hostName, portNum);

            NetworkStream ns = client.GetStream();

            byte[] bytes = new byte[1024];
            int bytesRead = ns.Read(bytes, 0, bytes.Length);
            
            Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));

            byte[] clientMsg = Encoding.ASCII.GetBytes("client2server msg");

            try
            {
                ns.Write(clientMsg, 0, clientMsg.Length);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return 0;
    }
}