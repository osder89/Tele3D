using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using PlanoGenerico.Extras;

namespace PlanoGenerico
{
    public class Cuadrado
    {
        private float ancho;
        private float alto;
        private float profundidad;
        public Punto origen;
        public Color4 color; // Utilizando Color4 de OpenTK
        private Punto punto;
        private Color4 colorTele;

        //colores personalizados 
        public Color4 ColorLeft { get; set; }
        public Color4 ColorRight { get; set; }
        public Color4 ColorFront { get; set; }
        public Color4 ColorBack { get; set; }
        public Color4 ColorBottom { get; set; }
        public Color4 ColorTop { get; set; }

        public Cuadrado(Punto p, float ancho, float alto, float profundidad, Color4 color,
                    Color4 leftColor, Color4 rightColor, Color4 frontColor,
                    Color4 backColor, Color4 bottomColor, Color4 topColor)
        {
            origen = p;
            this.ancho = ancho;
            this.alto = alto;
            this.profundidad = profundidad;
            this.color = color;
            ColorLeft = leftColor;
            ColorRight = rightColor;
            ColorFront = frontColor;
            ColorBack = backColor;
            ColorBottom = bottomColor;
            ColorTop = topColor;
        }

        public Cuadrado(Punto punto, float ancho, float alto, float profundidad, Color4 colorTele)
        {
            this.punto = punto;
            this.ancho = ancho;
            this.alto = alto;
            this.profundidad = profundidad;
            this.colorTele = colorTele;
        }

        public void DibujarT()
        {
            left(PrimitiveType.Quads);
            right(PrimitiveType.Quads);
            front(PrimitiveType.Quads);
            back(PrimitiveType.Quads);
            bottom(PrimitiveType.Quads);
            top(PrimitiveType.Quads);
        }

        private void right(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorRight);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z + profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z + profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z - profundidad);
            GL.End();
        }

        private void left(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorLeft);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z + profundidad);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z + profundidad);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z - profundidad);
            GL.End();
        }

        private void front(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorFront);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z + profundidad);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z + profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z + profundidad);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z + profundidad);
            GL.End();
        }

        private void back(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorBack);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z - profundidad);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z - profundidad);
            GL.End();
        }

        private void bottom(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorBottom);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y - alto, origen.z + profundidad);
            GL.Vertex3(origen.x - ancho, origen.y - alto, origen.z + profundidad);
            GL.End();
        }

        private void top(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color4(ColorTop);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z - profundidad);
            GL.Vertex3(origen.x + ancho, origen.y + alto, origen.z + profundidad);
            GL.Vertex3(origen.x - ancho, origen.y + alto, origen.z + profundidad);
            GL.End();
        }
    }
}
