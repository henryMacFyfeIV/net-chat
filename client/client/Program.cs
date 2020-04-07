using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class TcpTimeClient
{
    private const int portNum = 22740;
    private const string hostName = "127.0.0.1";

    public static int Main(String[] args)
    {
        while (true)
        {
            try
            {
                var client = new TcpClient(hostName, portNum);

                NetworkStream ns = client.GetStream();
                while (true) 
                {

                
                    System.Console.WriteLine("Send a msg to the server");
                    var clientMsg = Console.ReadLine();
                    if (clientMsg.Equals(".exit"))
                    {
                        break;
                    }
                    // byte[] bytes = new byte[1024];
                    // int bytesRead = ns.Read(bytes, 0, bytes.Length);
                    
                    // Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));

                    byte[] clientMsgByte = Encoding.ASCII.GetBytes("client2server msg");

                    try
                    {
                        ns.Write(clientMsgByte, 0, clientMsg.Length);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        return 0;
    }
}
