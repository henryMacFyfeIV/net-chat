# net-chat
Smit Patel and Henry Fyfe's semester project for Network/Info Assurance 3825-001

Instructions for part 3.
  1. Download microsoft dotnet core runtime 2.2
  2. Open a terminal and input commands below
      >cd net-chat/server/server &&
      >sudo dotnet run
  3. Keep the first terminal open and open a second terminal and input commands below
      >cd net-chat/client/client &&
      >dotnet run
  4. Everytime the client is run (shown above) a tcp connection is made with the tcp server,
     the tcp server logs these connections via their endpoint, responds with the current time,
     then closes the connection.

sources: 
https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-tcp-services
