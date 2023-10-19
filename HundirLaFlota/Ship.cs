using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirLaFlota
{
    internal class Ship
    {
        public int Lifes { get; set; }
        private List<Coordinates> coords;

        public Ship(int lifes, List<Coordinates> coords)
        {
            Lifes = lifes;
            this.coords = coords;
        }

        public Ship() { }

        public List<Coordinates> GetCoordinates() { return coords; }
    }

    public struct Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
