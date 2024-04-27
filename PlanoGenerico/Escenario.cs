using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PlanoGenerico
{
    public class Escenario
    {
        public Dictionary<string, Objeto> ObejtosEscenario { get; private set; }
        public Punto centro { get; set; }

        public Escenario()
        {
            ObejtosEscenario = new Dictionary<string, Objeto>();
            this.centro = new Punto (0, 0, 0);
        }

        public Escenario(Dictionary<string, Objeto> objetos, Punto centro) 
        {
            ObejtosEscenario = objetos;
            this.centro = centro;
            foreach (Objeto objeto in objetos.Values)
            {
                objeto.setEscenarioCentro(this.centro);
            }
        }

        public void AgregarObjeto(string nombreObjeto, Objeto Objeto) 
        {
            
            ObejtosEscenario.Add(nombreObjeto, Objeto);
        }

        public void DibujarEscenario()
        {
            foreach (Objeto Objeto in ObejtosEscenario.Values)
            {
                
                Objeto.DibujarObjeto(centro);
            }
        }
    }
}