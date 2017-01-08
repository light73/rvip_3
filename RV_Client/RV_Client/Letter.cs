using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RV_Client
{
    class Letter
    {
        public int name;
        public int text;

        public Letter(int _name)
        {
            name = _name;
        }
        
        public int Generate_letter()
        {
            Random r = new Random();
            Thread.Sleep(1);
            text = r.Next(1000, 10000);
            return text;
        }

        public void Send(int c, float sp)
        {                     
            Thread.Sleep(3);           
        }
    }
}
