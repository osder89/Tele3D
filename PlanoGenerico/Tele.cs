using OpenTK.Graphics;
using PlanoGenerico.Extras;

namespace PlanoGenerico
{
    public class Tele
    {
        private float ancho;
        private float alto;
        private float profundidad;
        public Punto origen;

        // Propiedades de color para cada parte del Tele
        public Color4 ColorTele { get; set; }
        public Color4 ColorSoporte { get; set; }
        public Color4 ColorBarra { get; set; }

        Cuadrado formaTele;
        Cuadrado formaSoporte;
        Cuadrado formaBarra;

        public Tele(Punto p, float ancho, float alto, float profundidad,
                         Color4 colorTele, Color4 colorSoporte, Color4 colorBarra)
        {
            origen = p;
            this.ancho = ancho;
            this.alto = alto;
            this.profundidad = profundidad;

            ColorTele = colorTele;
            ColorSoporte = colorSoporte;
            ColorBarra = colorBarra;

            float halfAncho = ancho / 2;
            float halfProfundidad = profundidad / 2;
            float halfAlto = alto / 2;

            // Crear las instancias de Cuadrado con los colores
            this.formaTele = new Cuadrado(
                new Punto(origen.x, origen.y + halfAlto, origen.z), // Punto superior
                ancho, alto, profundidad, ColorTele,
                Color4.Gray, Color4.Gray, ColorTele,
                Color4.Gray, Color4.Gray, Color4.DarkGray
            );

            this.formaBarra = new Cuadrado(
                new Punto(origen.x, origen.y - halfAlto +5/2 , origen.z +1 ), // Punto medio
                ancho, 2, profundidad, ColorBarra,
                Color4.Gray, ColorBarra, ColorBarra,
                ColorBarra, ColorBarra, ColorBarra
            );

            this.formaSoporte = new Cuadrado(
                new Punto(origen.x, origen.y - halfAlto , origen.z), // Punto inferior
                ancho / 2, alto / 2 - 10, profundidad, ColorSoporte,
                ColorSoporte, ColorSoporte, ColorSoporte,
                ColorSoporte, ColorSoporte, ColorSoporte
            );

        }

        public void Dibujar()
        {
            this.formaTele.DibujarT();
            this.formaSoporte.DibujarT();
            this.formaBarra.DibujarT();
        }
    }
}
