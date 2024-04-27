using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PlanoGenerico
{
    public class Objeto
    {
        public Dictionary<string, Partes> PartesObjeto { get; private set; }
        private Punto centro { get; set; }
        private Punto centroEscenario { get; set; }

        public Objeto()
        {
            PartesObjeto = new Dictionary<string, Partes>();
            this.centro = new Punto(0, 0, 0);
            this.centroEscenario = new Punto(0, 0, 0);
        }

        public Objeto(Dictionary<string, Partes> partes, Punto centro)
        {
            this.centro = centro;
            this.centroEscenario = new Punto(0, 0, 0);
            this.PartesObjeto = partes;   
        }

        public void setEscenarioCentro(Punto centroEscenario)
        {
            this.centroEscenario = centroEscenario; 
            foreach (var parte in PartesObjeto.Values)
            {
                parte.setCentroResto(this.centro + this.centroEscenario);
            }
        }

        public void AgregarPartes(string nombrePartes, Partes partes)  
        {
            
            PartesObjeto.Add(nombrePartes, partes);
        }

        public void DibujarObjeto(Punto escenarioCentro)
        {
            Punto centroObejto = escenarioCentro + this.centro;
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.DibujarFigura(centroObejto);
            }
        }
    }
}
