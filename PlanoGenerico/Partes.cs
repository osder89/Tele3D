using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PlanoGenerico
{
    public class Partes
    {
        public Dictionary<string, Cara> Figura { get; private set; }
        public Punto centroDeMasa { get; set; }

        public Partes()
        {
            Figura = new Dictionary<string, Cara>();
        }

        public Partes(Dictionary<string, Cara> caras, Punto CentroDeMasa)
        {
            Figura = caras;
            this.centroDeMasa = CentroDeMasa;
        }

        public void AgregarCara(string nombreCara, Cara cara) 
        {
            Figura.Add(nombreCara, cara);
            
        }

        public void DibujarFigura()
        {
            foreach (Cara cara in Figura.Values)
            {
                GL.PushMatrix(); 
                GL.Translate(centroDeMasa.x, centroDeMasa.y, centroDeMasa.z); 
                cara.dibujar();
                GL.PopMatrix(); 
            }
        }
        public void SumarCentroDeMasa(Punto centroDeMasaPadre)
        {
            this.centroDeMasa = new Punto(
                this.centroDeMasa.x + centroDeMasaPadre.x,
                this.centroDeMasa.y + centroDeMasaPadre.y,
                this.centroDeMasa.z + centroDeMasaPadre.z
            );
        }
    }
}
