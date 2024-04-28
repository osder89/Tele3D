using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PlanoGenerico
{
    public class Escenario
    {
        public Dictionary<string, Objeto> ObejtosEscenario { get; private set; }
        public Punto centroDeMasa { get; set; }

        public Escenario()
        {
            ObejtosEscenario = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> objeto, Punto centroDeMasa)
        {
            ObejtosEscenario = objeto;
            this.centroDeMasa = centroDeMasa;
        }

        public void AgregarObjeto(string nombreObjeto, Objeto Objeto)
        {
            ObejtosEscenario.Add(nombreObjeto, Objeto);
        }

        public void DibujarEscenario()
        {
            foreach (Objeto Objeto in ObejtosEscenario.Values)
            {
                Objeto.DibujarObjeto();
            }
        }
    }
}