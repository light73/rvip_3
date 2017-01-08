using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RV_Client
{
    class Camera
    {
        float av_speed = 0;
        int count = 0;
        const int slep = 80;
        int sl = 0;
        public int name;

        public Camera(int _name)
        {
            name = _name;
        }
        public void Survey()
        {

        }
        public float Speed_Fixing()
        {
            Random r = new Random();
            Thread.Sleep(10 * slep);
            av_speed = r.Next(10, 150);
            return av_speed;
        }
        public int Count_Fixing()
        {
            Random r = new Random();
            Thread.Sleep(10 * slep);
            count = r.Next(1, 200);
            return count;
        }
        public void Send(int c, float sp)
        {
            Random r = new Random();
            sl = r.Next(10, 100);
            Thread.Sleep(sl * slep);
           // Get_info(sp, c);
        }
    }
}
