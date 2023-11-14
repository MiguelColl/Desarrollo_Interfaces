using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public class Pizza
    {
        double precio;

        public Pizza()
        {
            precio = 0;
        }

        public double GetPrecio() { return precio; }

        public double AddIngredient(int ingredient)
        {
            int[] ingredients = { 5, 6, 6, 7, 7, 7, 10 };
            if (ingredient < 1 || ingredient > 7)
                throw new InvalidOptionsException();
            else
                precio = ingredients[ingredient - 1];
            
            return precio;
        }

        public double AddSize(int size)
        {
            double[] sizes = { 0.8, 1, 1.2 };

            if (size < 1 || size > 3)
                throw new InvalidOptionsException();
            else
                precio *= sizes[size - 1];

            return precio;
        }

        public double AddDelivery(int delivery)
        {
            if (delivery == 1)
                precio += 2;
            else if(delivery < 1 || delivery > 2)
                throw new InvalidOptionsException();

            return precio;
        }
    }
}
