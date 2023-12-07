using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorizarPersona
{
    internal class Interfaz
    {
        public void MostrarPersona(Persona p)
        {
            Console.WriteLine("Nombre: {0}", p.Nombre);
            Console.WriteLine("Edad: {0}", p.Edad);
            Console.WriteLine("Dirección: {0}", p.Direccion);
            Console.WriteLine("IMC: {0}", p.CalcularIMC());
        }
    }
}
