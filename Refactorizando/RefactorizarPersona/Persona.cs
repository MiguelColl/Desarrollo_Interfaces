using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorizarPersona
{
    internal class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }

        public Persona(string nombre, int edad, string direccion, float peso, float altura)
        {
            Nombre = nombre;
            Edad = edad;
            Direccion = direccion;
            Peso = peso;
            Altura = altura;
        }


        public float CalcularIMC()
        {
            return Peso / (Altura * Altura);
        }

        public void ActualizarDatos(string nombre, int edad, string direccion)
        {
            Nombre = nombre;
            Edad = edad;
            Direccion = direccion;
        }


        /* ---- CLASE A REFACTORIZAR ----
 
        public class Persona
        {

            public string Nombre { get; set; }
            public int Edad { get; set; }
            public string Dirección { get; set; }

            public void MostrarDatos()
            {
                Console.WriteLine("Nombre: {0}", Nombre);
                Console.WriteLine("Edad: {0}", Edad);
                Console.WriteLine("Dirección: {0}", Dirección);
            }

            public void CalcularIMC()
            {
                int peso = 75;
                int altura = 180;
                float imc = peso / (altura * altura);
                Console.WriteLine("IMC: {0}", imc);
            }

            public void ActualizarDatos()
            {
                // Obtiene los datos del usuario
                Console.WriteLine("Introduce tu nombre:");
                Nombre = Console.ReadLine();
                Console.WriteLine("Introduce tu edad:");
                Edad = int.Parse(Console.ReadLine());
                Console.WriteLine("Introduce tu dirección:");
                Dirección = Console.ReadLine();
            }
        }
        */
    }
}
