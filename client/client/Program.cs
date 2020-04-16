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
                    // read from server

                    if(ns.DataAvailable)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRead = ns.Read(bytes, 0, bytes.Length);
                        Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));        
                    }
                    
                    
                    // var noNewData = true;
                    // while (noNewData)
                    // {
                    //     if(ns.DataAvailable)
                    //     {
                    //         noNewData = false;
                    //     }
                    // }
                    // byte[] bytes = new byte[1024];
                    // int bytesRead = ns.Read(bytes, 0, bytes.Length);
                    // Console.WriteLine(Encoding.ASCII.GetString(bytes,0,bytesRead));
                    
                    System.Console.WriteLine( "Either send a msg like this: targetId youre message here. Just press enter to read your pending msgs. Or type exit()");
                    var clientMsg = Console.ReadLine();
                    if (clientMsg.Length > 1) 
                    {
                        var senderId = client.Client.LocalEndPoint.ToString();
                        clientMsg = clientMsg + " ~signed: " + senderId.Split(':')[1];
                        if (clientMsg.Contains("exit()"))
                        {
                            client.Close();
                            Environment.Exit(0);
                        }
                        // send msg to client
                        byte[] clientMsgByte = Encoding.ASCII.GetBytes(clientMsg);
                        try
                        {
                            ns.Write(clientMsgByte, 0, clientMsg.Length);
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
