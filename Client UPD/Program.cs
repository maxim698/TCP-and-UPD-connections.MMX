using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_UPD
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";
            const int PORT = 8082;
            const int SIZE_BUFFER = 256;
            const int SERVER_PORT = 8081;

            var updEndPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            var updSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            updSocket.Bind(updEndPoint);
            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var message = Console.ReadLine();
                var serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), SERVER_PORT);
                updSocket.SendTo(Encoding.UTF8.GetBytes(message), serverEndPoint);

                var buffer = new byte[SIZE_BUFFER];

                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse(IP), SERVER_PORT);
                do
                {
                    int size;
                    size = updSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));

                } while (updSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();
            }
        }
    }
}
