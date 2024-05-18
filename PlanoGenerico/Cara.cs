
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

using OpenTK.Graphics;


namespace PlanoGenerico
{
    public class Cara
    {
        public List<Punto> listaDePuntos { get; set; }
        public string nombre { get; set; }
        public Color4 color { get; set; }

        private Matrix4 transformaciones;
        private Matrix4 transformacionesInversas;

        public Cara()
        {
            listaDePuntos = new List<Punto>();
            color = Color4.Black;
            nombre = "";
            transformaciones = Matrix4.Identity;
            transformacionesInversas = Matrix4.Identity;
        }

        public Cara(List<Punto> lista, Color4 color)
        {
            listaDePuntos = lista;
            this.color = color;
            nombre = "";
            transformaciones = Matrix4.Identity;
            transformacionesInversas = Matrix4.Identity;
        }

        public void addPunto(Punto vertice)
        {
            listaDePuntos.Add(vertice);
        }

        public void Trasladar(float x, float y, float z)
        {
            transformaciones = Matrix4.Mult(transformaciones, Matrix4.CreateTranslation(x, y, z));
            transformacionesInversas = Matrix4.Mult(Matrix4.CreateTranslation(-x, -y, -z), transformacionesInversas);
        }

        public void Scalar(float n)
        {
            transformaciones = Matrix4.Mult(transformaciones, Matrix4.CreateScale(n));
            transformacionesInversas = Matrix4.Mult(Matrix4.CreateScale(1.0f / n), transformacionesInversas);
        }

        public void ScalarCentroDeMasa(Punto origin, float n)
        {
            Trasladar(-origin.x, -origin.y, -origin.z);
            Scalar(n);
            Trasladar(origin.x, origin.y, origin.z);
        }

        public void Rotar(string axis, float grades)
        {
            float radians = MathHelper.DegreesToRadians(grades);
            Matrix4 rotationMatrix = Matrix4.Identity;

            if (axis == "x")
                rotationMatrix = Matrix4.CreateRotationX(radians);
            else if (axis == "y")
                rotationMatrix = Matrix4.CreateRotationY(radians);
            else if (axis == "z")
                rotationMatrix = Matrix4.CreateRotationZ(radians);

            transformaciones = Matrix4.Mult(transformaciones, rotationMatrix);
            transformacionesInversas = Matrix4.Mult(rotationMatrix, transformacionesInversas);
        }

        public void RotarCentroDeMasa(Punto origin, string axis, float grades)
        {
            Trasladar(-origin.x, -origin.y, -origin.z);
            Rotar(axis, grades);
            Trasladar(origin.x, origin.y, origin.z);
        }

        public void dibujar()
        {
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);

            foreach (Punto punto in listaDePuntos)
            {
                Vector4 puntoTransformado = Vector4.Transform(new Vector4(punto.x, punto.y, punto.z, 1.0f), transformaciones);
                GL.Vertex3(puntoTransformado.X, puntoTransformado.Y, puntoTransformado.Z);
            }

            GL.End();
        }
    }
}
