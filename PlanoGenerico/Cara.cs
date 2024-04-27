
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



        public Cara()
        {
            listaDePuntos = new List<Punto>();
            this.color = Color4.Black; 
            this.nombre = "";
        }
        public Cara(List<Punto> lista, Color4 color)
        {
            this.listaDePuntos = lista;
            this.color = color;

        }

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
