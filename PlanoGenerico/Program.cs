using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoGenerico
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (Grafico Grafico = new Grafico(1080, 720, "Tele"))
            {
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                Grafico.Run(100.0);
            }
        }
    }
}
