using System;
using Newtonsoft.Json;
using System.IO;


namespace PlanoGenerico
{
    class Serializacion
    {
        
        public void GuardarEscenario(string nombreArchivo, Escenario escenario)
        {
            string json = JsonConvert.SerializeObject(escenario);
            File.WriteAllText(nombreArchivo, json);
            Console.WriteLine("Escenario guardado correctamente en " + nombreArchivo);
        }

        public  Escenario CargarEscenario(string nombreArchivo)
        {
            string json = File.ReadAllText(nombreArchivo);
            Escenario escenario = JsonConvert.DeserializeObject<Escenario>(json);
            string jsonEscenario = JsonConvert.SerializeObject(escenario, Formatting.Indented);
            Console.WriteLine(jsonEscenario);
            return escenario;
        }
    }
}
