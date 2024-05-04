using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PlanoGenerico
{
    public class Escenario
    {
        public Dictionary<string, Objeto> ObjetosEscenario { get; private set; }
        public Punto centroDeMasa { get; set; }

        public Escenario()
        {
            ObjetosEscenario = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> objeto, Punto centroDeMasa)
        {
            ObjetosEscenario = objeto;
            this.centroDeMasa = centroDeMasa;
        }

        public void AgregarObjeto(string nombreObjeto, Objeto Objeto)
        {
            Objeto.centroDeMasa += centroDeMasa;
            ObjetosEscenario.Add(nombreObjeto, Objeto);
        }

        public Objeto ObtenerObjetoPorNombre(string nombreObjeto)
        {
            if (ObjetosEscenario.ContainsKey(nombreObjeto))
            {
                return ObjetosEscenario[nombreObjeto];
            }
            else
            {
                return null; 
            }
        }

        public void TransladarEscenario(float x, float y, float z)
        {
            centroDeMasa = new Punto(centroDeMasa.x + x, centroDeMasa.y + y, centroDeMasa.z + z);

            foreach (Objeto objeto in ObjetosEscenario.Values)
            {
                objeto.TransladarObjeto(x, y, z);
            }
        }

        public void ScalarEscenario(float n)
        {
            foreach (Objeto objeto in ObjetosEscenario.Values)
            {
                objeto.ScalarObjeto(n);
            }
        }

        public void ScalarCentroDeMasa(Punto origin, float n)
        {
            foreach (Objeto objeto in ObjetosEscenario.Values)
            {
                objeto.ScalarCentroDeMasa(origin, n);
            }
        }

        public void RotarEscenario(string axis, float grades)
        {
            foreach (Objeto objeto in ObjetosEscenario.Values)
            {
                objeto.RotarObjeto(axis, grades);
            }
        }

        public void RotarCentroDeMasa(Punto origin, string axis, float grades)
        {
            foreach (Objeto objeto in ObjetosEscenario.Values)
            {
                objeto.RotarConCentroDeMasa(origin, axis, grades);
            }
        }

        public void DibujarEscenario()
        {
            foreach (Objeto Objeto in ObjetosEscenario.Values)
            {
                Objeto.DibujarObjeto();
            }
        }

       
    }
}