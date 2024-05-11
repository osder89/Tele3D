using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;


namespace PlanoGenerico
{
    class Animacion
    {
       
            private Objeto objeto;
            private List<Transformacion> listaDeTransformaciones;
            private float tiempoEntreTransformaciones; // Tiempo en segundos

            public Animacion(Objeto objeto, float tiempoEntreTransformaciones)
            {
                this.objeto = objeto;
                listaDeTransformaciones = new List<Transformacion>();
                this.tiempoEntreTransformaciones = tiempoEntreTransformaciones;
            }

            public void AgregarTransformacion(Transformacion transformacion)
            {
                listaDeTransformaciones.Add(transformacion);
            }

        public async Task EjecutarAnimacion()
        {
            foreach (Transformacion transformacion in listaDeTransformaciones)
            {
                await Task.Delay((int)(tiempoEntreTransformaciones * 1000)); // Esperar el tiempo entre transformaciones

                AplicarTransformacion(transformacion);
            }
        }

        private void AplicarTransformacion(Transformacion transformacion)
            {
                if (transformacion.Tipo == "Traslacion")
                {
                    objeto.TransladarObjeto(transformacion.CantidadX, transformacion.CantidadY, transformacion.CantidadZ);
                }
                else if (transformacion.Tipo == "Escalado")
                {
                    objeto.ScalarCentroDeMasa(transformacion.Origen, transformacion.CantidadEscala);
                }
                else if (transformacion.Tipo == "Rotacion")
                {
                    objeto.RotarConCentroDeMasa(transformacion.Origen, transformacion.Eje, transformacion.Grados);
                }
            }
        

    }
}
