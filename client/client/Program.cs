using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class TcpTimeClient
{
    private const int portNum = 22740;
    private const string hostName = "127.0.0.1";

    public static void Main(String[] args)
    {
        while (true)
        {
            try
            {
                var client = new TcpClient(hostName, portNum);

                NetworkStream ns = client.GetStream();
                while (true) 
                {
                    System.Console.WriteLine("enter loop");

                    // read server intro
                    byte[] bytes = new byte[1024];
                    int bytesRead = ns.Read(bytes, 0, bytes.Length);
                    Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));
                    
                    System.Console.WriteLine( "ready to read line");
                    var clientMsg = Console.ReadLine();
                    System.Console.WriteLine("readline");
                    if (clientMsg.Contains("exit"))
                    {
                        System.Console.WriteLine("fuck off time");
                        client.Close();
                    }
                    

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
                // client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
