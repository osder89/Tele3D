
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

using OpenTK.Graphics;





namespace PlanoGenerico
{
    public class Cara
    {
        public List<Punto> listaDePuntos { get; set; }
        public String nombre { get; set; }
        public Color4 color { get; set; }
        private Punto centroC;
        private Punto centroAcarreado;



        public Cara()
        {
            listaDePuntos = new List<Punto>();
            this.centroC = new Punto(0, 0, 0);
            this.centroAcarreado = new Punto(0, 0, 0);
            this.color = Color.Black; 
            this.nombre = "";
        }
        public Cara(List<Punto> lista, Color color)
        {
            this.centroC = new Punto(0, 0, 0);
            this.centroAcarreado = new Punto(0, 0, 0);
            this.listaDePuntos = lista;
            this.color = color;

        }

       /* public void setCentroAcarreado(Punto centroAcarreado)
        {
            this.centroAcarreado = centroAcarreado + centroC;
            this.matriz.SetCentro(this.centroAcarreado.X, this.centroAcarreado.Y, this.centroAcarreado.Z);
        }*/

        public void addPunto(Punto vertice)
        {
            this.listaDePuntos.Add(vertice);
        }
        public List<Punto> getCara()
        {
            return this.listaDePuntos;
        }
        //dibuja la cara
        public void dibujar()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(color);
            foreach (Punto punto in this.listaDePuntos)
            {
                GL.Vertex3(punto.getX(), punto.getY(), punto.getZ());
            }
            GL.End();
        }
    }

}
