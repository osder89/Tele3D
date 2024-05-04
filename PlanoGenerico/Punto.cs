using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PlanoGenerico
{
    public class Punto
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public Punto(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void setX(float x) { this.x = x; }
        public void setY(float y) { this.y = y; }
        public void setZ(float z) { this.z = z; }
        public float getX()
        {
            return this.x;
        }
        public float getY() { return this.y; }
        public float getZ() { return this.z; }

        public static Punto operator +(Punto punto1, Punto punto2)
        {
            return new Punto(punto1.x + punto2.x, punto1.y + punto2.y, punto1.z + punto2.z);
        }

        public void TrasladarPunto(Vector3 translation)
        {
            x += translation.X;
            y += translation.Y;
            z += translation.Z;
        }

        public void RotarPunto(float angleX, float angleY, float angleZ)
        {
            // Implementa la rotación en torno a los ejes X, Y y Z según los ángulos proporcionados
        }

        public void EscalarPunto(float scaleX, float scaleY, float scaleZ)
        {
            x *= scaleX;
            y *= scaleY;
            z *= scaleZ;
        }


    }

}
