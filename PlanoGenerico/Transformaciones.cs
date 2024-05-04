using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoGenerico
{
    public enum TipoTransformacion
    {
        Escala,
        Traslacion,
        Rotacion
    }
    class Transformaciones
    {
        public TipoTransformacion Tipo { get; set; }
        public float Parametro { get; set; }
    }
}
