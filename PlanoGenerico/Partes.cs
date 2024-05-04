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



        public void TransladarCaras(float x, float y, float z)
        {
            foreach (Cara cara in Figura.Values)
            {
                cara.Trasladar(x, y, z);
            }
        }

        public void ScalarCaras(float n)
        {
            foreach (Cara cara in Figura.Values)
            {
                cara.Scalar(n);
            }
        }

        public void ScalarCentroDeMasa(Punto origin, float n)
        {
            foreach (Cara cara in Figura.Values)
            {
                cara.ScalarCentroDeMasa(origin, n);
            }
        }

        public void RotarCaras(string axis, float grades)
        {
            foreach (Cara cara in Figura.Values)
            {
                cara.Rotar(axis, grades);
            }
        }

        public void RotarCentroDeMasa(Punto origin, string axis, float grades)
        {
            foreach (Cara cara in Figura.Values)
            {
                cara.RotarCentroDeMasa(origin, axis, grades);
            }
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

    }
}
