namespace RefactorizarPersona
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Interfaz i = new Interfaz();

            // Obtiene los datos del usuario
            Console.WriteLine("Introduce tu nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduce tu edad:");
            int edad = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduce tu dirección:");
            string dirección = Console.ReadLine();
            Console.WriteLine("Introduce tu peso:");
            float peso = float.Parse(Console.ReadLine());
            Console.WriteLine("Introduce tu peso:");
            float altura = float.Parse(Console.ReadLine());
            Console.WriteLine();

            Persona p = new Persona(nombre, edad, dirección, peso, altura);
            i.MostrarPersona(p);

            Console.WriteLine();
            Console.WriteLine("Cambiamos los datos");
            p.ActualizarDatos("Pepe", 32, "c/desconocida");
            i.MostrarPersona(p);
        }
    }
}