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
            centroDeMasa = new Punto(0, 0, 0); // Inicializar el centro de masa del objeto
        }

        public Objeto(Dictionary<string, Partes> partes, Punto centroDeMasa)
        {
            PartesObjeto = partes;
            this.centroDeMasa = centroDeMasa;
        }

        public void AgregarPartes(string nombrePartes, Partes partes)
        {
            partes.centroDeMasa =  partes.centroDeMasa + centroDeMasa; 
            PartesObjeto.Add(nombrePartes, partes);
        }

        public Partes ObtenerPartePorNombre(string nombreParte)
        {
            if (PartesObjeto.ContainsKey(nombreParte))
            {
                return PartesObjeto[nombreParte];
            }
            else
            {
                return null; 
            }
        }

        public void TransladarObjeto(float x, float y, float z)
        {
            centroDeMasa = new Punto(centroDeMasa.x + x, centroDeMasa.y + y, centroDeMasa.z + z);

            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.TransladarCaras(x, y, z);
            }
        }

        public void ScalarObjeto(float n)
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.ScalarCaras(n);
            }
        }

        public void ScalarCentroDeMasa(Punto origin, float n)
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.ScalarCentroDeMasa(origin, n);
            }
        }

        public void RotarObjeto(string axis, float grades)
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.RotarCaras(axis, grades);
            }
        }

        public void RotarConCentroDeMasa(Punto origin, string axis, float grades)
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.RotarCentroDeMasa(origin, axis, grades);
            }
        }

        public void DibujarObjeto()
        {
            foreach (Partes partes in PartesObjeto.Values)
            {
                partes.DibujarFigura();
            }
        }

        

    }
}
