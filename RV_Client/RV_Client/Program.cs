using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace RV_Client
{
    class Program
    {
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        
        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);

                    Random rand_serm = new Random();
                    
                    Letter let = new Letter(Convert.ToInt32(rand_serm.Next(0, 10)));
                    int text, name;
                    name = let.name;
                    text = let.Generate_letter();
                    
                    Console.WriteLine(name.ToString() + " отправляет данные: Письмо " + text.ToString());

                    string message = name.ToString() + " " + text.ToString();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("Ответ SMPT-сервера: " + builder.ToString());

                    // закрываем сокет
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

    }
}
