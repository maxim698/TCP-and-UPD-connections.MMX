using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server_UPD
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";
            const int PORT = 8081;
            var endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);

            var updSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            updSocket.Bind(endPoint);
            while (true)
            {
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                do
                {
                    size = updSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                } while (updSocket.Available > 0);

                updSocket.SendTo(Encoding.UTF8.GetBytes("Успех,сообщение получено"), senderEndPoint);
                Console.WriteLine(data);
            }
        }
    }
}
