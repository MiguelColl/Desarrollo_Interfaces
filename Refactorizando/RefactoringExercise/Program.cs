namespace RefactoringExercise
{
    public class InvalidOptionsException : Exception
    {
        public InvalidOptionsException() : base("La opción elegida no está en el rango válido") { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = new Pizza();

            ShowMenu(1);
            pizza.AddIngredient(int.Parse(Console.ReadLine()));
            Console.WriteLine();

            ShowMenu(2);
            pizza.AddSize(int.Parse(Console.ReadLine()));
            Console.WriteLine();

            ShowMenu(3);
            pizza.AddDelivery(int.Parse(Console.ReadLine()));
            Console.WriteLine();

            Console.WriteLine($"El precio de tu pizza es {pizza.GetPrecio()} euros");
        }

        private static void ShowMenu(int menu)
        {
            switch (menu)
            {
                case 1:
                    Console.WriteLine("Bienvenido a la pizzería");
                    Console.WriteLine("1. Queso");
                    Console.WriteLine("2. Jamón");
                    Console.WriteLine("3. Champiñones");
                    Console.WriteLine("4. Salami");
                    Console.WriteLine("5. Piña");
                    Console.WriteLine("6. Aceitunas");
                    Console.WriteLine("7. Todos");
                    Console.Write("¿Qué ingredientes quieres en tu pizza? ");
                    break;
                case 2:
                    Console.WriteLine("1. Pequeña");
                    Console.WriteLine("2. Mediana");
                    Console.WriteLine("3. Grande");
                    Console.Write("¿Qué tamaño quieres? ");
                    break;
                case 3:
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("2. No");
                    Console.Write("¿Quieres que te la entreguen a domicilio? ");
                    break;
            }
        }
    }
}