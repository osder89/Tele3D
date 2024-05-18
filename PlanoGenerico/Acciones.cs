using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoGenerico
{
    class Acciones
    {
        public List<object> objeto { get; set; }
        public int tiempo { get; set; }
        public Acciones(List<object> accion, int tiempoSegundos)
        {
            objeto = accion;
            tiempo = tiempoSegundos;
        }
        public Acciones()
        {
            this.objeto = new List<object>();
            this.tiempo = 0;
        }


    }
}
