using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server_TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";//айпи адрес нашего сервера
            const int PORT = 8080;//порт через который будут передаваться данный на наш сервер
            const int SIZE_BUFFER = 256;//количество байт для буфера данных
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);//создание точки доступа на наш сервер

            var socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//создаем сокет,своеобразную дверцу для входа на наш сервер
                                                                                                        //набор параметров для TCP соединения всегда одинаковый
            socketTcp.Bind(tcpEndPoint);//связываем нашу точку доступа сокетом с помощью метода BIND
            socketTcp.Listen(5);//УСтановка сколько клиентов может обрабатывать наш сервер за раз

            while (true)
            {
                var listener = socketTcp.Accept();//создаем новый сокет под каждый запрос клиента,после чего удаляем его.ОБязательно в бесконечном цикле.
                var buffer = new byte[SIZE_BUFFER];//создаем байтовый массив для использования его как буфера данный,которые передаются между сокетами.Данные передаются
                                                   //в двоичном формате поэтому массив байтовый
                var size = 0;//переменная для записи реального количества байтов переданных между сокетами.
                var data = new StringBuilder();//создаем собирателя наших данных полученных от пользователя в формат строки
                do
                {
                    size = listener.Receive(buffer);//записываем в переменную size реальное количество байт из каждого запроса пользователя
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));//раскодируем данные полученные от пользователя в запросе в строки по блокам равных
                                                                          // количеству байт "buffer" объединяя их в строку создавая запрос на сервер
                } while (listener.Available > 0); //продолжаем этот цикл пока запрос пользователя не будет пустым.

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("ПОдключение создано!"));//генерируем ответ пользователю от сервера при успешном подключение
                listener.Shutdown(SocketShutdown.Both);//выключаем сокет после обработки запроса
                listener.Close();//Закрываем сокет
            }
        }
    }
}
