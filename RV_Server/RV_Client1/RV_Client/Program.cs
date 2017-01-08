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
        const int MaxCountCam = 2;
        const int MaxCountTrans = 10;

        static void Main(string[] args)
        {
             try
            {
                for (int i = 0; i < MaxCountTrans; i++)
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);


                    Camera cam = new Camera(2);
                    int count; float sp;
                    sp = cam.Speed_Fixing();
                    count = cam.Count_Fixing();
                    Console.WriteLine("Камера" + cam.name.ToString() + " отправляет данные: Средняя скорость " + sp.ToString() + " , количество машин " + count.ToString());

                    string message = sp.ToString() + " " + count.ToString();
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
                    Console.WriteLine("ответ сервера: " + builder.ToString());

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
