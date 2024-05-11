using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoGenerico
{
    class Transformacion
    {
        public string Tipo { get; set; }
        public float CantidadX { get; set; }
        public float CantidadY { get; set; }
        public float CantidadZ { get; set; }
        public float CantidadEscala { get; set; }
        public Punto Origen { get; set; }
        public string Eje { get; set; }
        public float Grados { get; set; }

        public Transformacion(string tipo, float cantidadX, float cantidadY, float cantidadZ)
        {
            Tipo = tipo;
            CantidadX = cantidadX;
            CantidadY = cantidadY;
            CantidadZ = cantidadZ;
        }

        public Transformacion(string tipo, float cantidadEscala, Punto origen)
        {
            Tipo = tipo;
            CantidadEscala = cantidadEscala;
            Origen = origen;
        }

        public Transformacion(string tipo, string eje, float grados, Punto origen)
        {
            Tipo = tipo;
            Eje = eje;
            Grados = grados;
            Origen = origen;
        }
    }
}
