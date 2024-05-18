using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using OpenTK;

namespace PlanoGenerico
{
    public class ObjetoFisico
    {
        public Objeto Objeto { get; set; }
        public float Masa { get; set; }
        public Vector3 Velocidad { get; set; }
        public Vector3 Aceleracion { get; set; }

        public ObjetoFisico(Objeto objeto, float masa)
        {
            Objeto = objeto;
            Masa = masa;
            Velocidad = Vector3.Zero;
            Aceleracion = Vector3.Zero;
        }

        public void AplicarFuerza(Vector3 fuerza)
        {
            Aceleracion = fuerza / Masa;
        }

        public void Actualizar(float deltaTime)
        {
            Velocidad += Aceleracion * deltaTime;
            //Objeto.Posicion += Velocidad * deltaTime;
        }
    }

}
