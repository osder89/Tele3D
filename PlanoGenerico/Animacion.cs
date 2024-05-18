using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using OpenTK;

namespace PlanoGenerico

{



    /*class Animacion
    {
        private Objeto objeto;
        private List<Transformacion> listaDeTransformaciones;
        private float tiempoEntreTransformaciones; // Tiempo en segundos
        private bool enPausa;
        private bool animacionEnProgreso;

        public Animacion(Objeto objeto, float tiempoEntreTransformaciones)
        {
            this.objeto = objeto;
            listaDeTransformaciones = new List<Transformacion>();
            this.tiempoEntreTransformaciones = tiempoEntreTransformaciones;
            enPausa = false;
            animacionEnProgreso = false;
        }

        public void AgregarTransformacion(Transformacion transformacion)
        {
            listaDeTransformaciones.Add(transformacion);
        }

        public async Task EjecutarAnimacion()
        {
            animacionEnProgreso = true;
            foreach (Transformacion transformacion in listaDeTransformaciones)
            {
                if (enPausa)
                {
                    while (enPausa) // Espera hasta que se reanude la animación
                    {
                        await Task.Delay(100); // Espera 100 milisegundos
                    }
                }

                await Task.Delay((int)(transformacion.Duracion * 1000)); // Esperar la duración de la transformación

                AplicarTransformacion(transformacion);
            }
            animacionEnProgreso = false;
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

        public void PausarAnimacion()
        {
            enPausa = true;
        }

        public void ReanudarAnimacion()
        {
            enPausa = false;
        }

        public bool EstaEnPausa()
        {
            return enPausa;
        }

        public bool AnimacionEnProgreso()
        {
            return animacionEnProgreso;
        }
    }*/





































    class Animacion
    {
        public List<Acciones> acciones { get; set; }
        private Thread ejecucionHilo;
        private bool pausado;
        private bool detener;

        public Animacion()
        {
            acciones = new List<Acciones>();
            ejecucionHilo = null;
            pausado = false;
            detener = false;
        }
        public Animacion(Animacion animacion)
        {
            acciones = new List<Acciones>();
            foreach (Acciones act in animacion.acciones)
            {
                acciones.Add(act);
            }
            ejecucionHilo = null;
            pausado = false;
            detener = false;
        }
        public void AgregarAccion(List<object> accion, int tiempo)
        {
            acciones.Add(new Acciones(accion, tiempo));
        }

        public void Iniciar()
        {
            if (ejecucionHilo == null || !ejecucionHilo.IsAlive)
            {
                pausado = false;
                detener = false;
                ejecucionHilo = new Thread(EjecutarAcciones);
                ejecucionHilo.Start();
            }
            else if (pausado)
            {
                pausado = false;
            }
        }

        public void Pausar()
        {
            pausado = true;
        }

        public void Detener()
        {
            detener = true;
        }

        private void EjecutarAcciones()
        {
            foreach (Acciones accion in acciones)
            {
                int tiempoRestante = accion.tiempo;

                while (tiempoRestante > 0)
                {
                    if (detener)
                    {
                        return;
                    }

                    if (!pausado)
                    {
                        // Ejecutar la acción
                        for (int i = 0; i < accion.objeto.Count; i++)
                        {
                            object obj = accion.objeto[i];
                            obj.GetType().GetMethod("Invoke").Invoke(obj, null);
                        }
                        // Esperar 1 segundo (1000 milisegundos)
                        Thread.Sleep(50);
                        tiempoRestante--;
                    }
                    else
                    {
                        Thread.Sleep(0);//Lo que se espera para la otra acción
                    }
                }
            }
        }

    }
}
