using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_and_UPD_connections.MMX
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";

            const int PORT = 8080;

            const int SIZE_BUFFER = 256;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);

            var socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (true)
            {


                Console.WriteLine("Введите сообщение!");


                var messege = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(messege);//Задаем кодировку нашим данным,чтобы сервер мог и распознать
                socketTcp.Connect(tcpEndPoint);//соединяемся с сервером с помощью метода COnect через точку доступа
                socketTcp.Send(data);//с помощью метода SEnd вводим наши данные серверу



                var buffer = new byte[SIZE_BUFFER];

                var size = 0;

                var answer = new StringBuilder();

                do
                {
                    size = socketTcp.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (socketTcp.Available > 0);

                Console.WriteLine(answer.ToString());

                socketTcp.Shutdown(SocketShutdown.Both);
                socketTcp.Close();

                Console.ReadLine();
            }
        }
    }
}
