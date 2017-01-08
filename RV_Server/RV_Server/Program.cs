using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace RV_Server
{
    class Program
    {
        static int port = 8005;

        static void Main(string[] args)
        {
            ConcurrentBag<string> letter = new ConcurrentBag<string>();
            ConcurrentBag<string> text = new ConcurrentBag<string>();

            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("SMPT-cервер запущен. Ожидание получение писем...");
                int endl = 29;
                while (endl>0)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    String[] mas = builder.ToString().Split(' ');
                    letter.Add(mas[0]);
                    text.Add(mas[1]);                  
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ":Сервер принимает: Отправитель " + mas[0].ToString() + "  Письмо: " + mas[1].ToString());

                    // отправляем ответ
                    string message = "Ваше сообщение принято";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    endl--;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

    }
 }
