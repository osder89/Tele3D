using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PlanoGenerico
{
    public class Objeto
    {
        public Dictionary<string, Partes> PartesObjeto { get; private set; }
        public Punto centroDeMasa { get; set; }

        public Objeto()
        {
            PartesObjeto = new Dictionary<string, Partes>();
        }

        public Objeto(Dictionary<string, Partes> partes, Punto centroDeMasa)
        {
            PartesObjeto = partes;
            this.centroDeMasa = centroDeMasa;
        }

        public void AgregarPartes(string nombrePartes, Partes partes)
        {
            PartesObjeto.Add(nombrePartes, partes);
        }

        public void DibujarObjeto()
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.SumarCentroDeMasa(centroDeMasa);
                partes.DibujarFigura();
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
