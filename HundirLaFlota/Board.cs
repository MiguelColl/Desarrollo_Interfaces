using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirLaFlota
{
    public enum Characters : int
    {
        Sea = '~',
        Missed = 'X',
        Touched = 'T',
        Destroyed = 'D',
        Ship = 'S'
    }

    internal class Board
    {
        private const int BOARD_SIZE = 10;
        private char[,] board = new char[BOARD_SIZE, BOARD_SIZE];
        private bool isEnemy;
        private List<Ship> ships;
        private readonly int[] SHIP_SIZES = { 5, 4, 3, 3, 2 };

        public Board(bool enemy)
        {
            for (int i = 0; i < BOARD_SIZE; i++) // We full our board with '~' (sea)
                for (int j = 0; j < BOARD_SIZE; j++)
                    board[i, j] = (char)Characters.Sea;

            this.isEnemy = enemy;
            ships = new List<Ship>();
        }

        public int GetShipsLength() { return this.ships.Count; }

        public int GetShipSizes() { return this.SHIP_SIZES.Length; }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if(isEnemy)
                Console.WriteLine("    ENEMIGO");
            else
                Console.WriteLine("    JUGADOR");

            Console.ForegroundColor = ConsoleColor.White;

            int letter = 65; // Letter A
            for (int i = 0; i < BOARD_SIZE + 1; i++) // Board shifted down 1 step
            {
                for (int j = 0; j < BOARD_SIZE + 2; j++) // Board shifted to right 2 step
                {
                    if (j == 1) // Column 1
                        Console.Write("|");
                    else if (i == 0) // Row 0, numbers 1 - 9 and X
                    {
                        if (j == 0)
                            Console.Write(" ");
                        else if (j == BOARD_SIZE + 1)
                            Console.Write("X");
                        else
                            Console.Write(j - 1);
                    }
                    else if (j == 0) // Column 0, letters A - J
                    {
                        Console.Write(Convert.ToChar(letter));
                        letter++;
                    }
                    else // Main board
                    {
                        if (board[i - 1, j - 2] == (char)Characters.Ship) // Ships(S) printed in green
                        {
                            if (isEnemy) // We hide the board if is an enemy
                            {
                                Console.Write("?");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(board[i - 1, j - 2]);
                            }
                        }
                        else
                        {
                            // Change colors
                            if (board[i - 1, j - 2] == (char)Characters.Missed)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else if (board[i - 1, j - 2] == (char)Characters.Touched)
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            else if (board[i - 1, j - 2] == (char)Characters.Destroyed)
                                Console.ForegroundColor = ConsoleColor.Red;
                             
                            // Print 
                            if(isEnemy && board[i - 1, j - 2] == (char)Characters.Sea) // We hide the board if is an enemy
                                Console.Write("?");
                            else
                                Console.Write(board[i - 1, j - 2]);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public bool CheckValidCoord(string coord) // Method to check if a string is a valid coordinate
        {
            bool correct = true;
            if (coord.Length != 2) // Length must be 2
                correct = false;
            else
            {
                char letter = Char.ToUpper(coord[0]); // The first char of the coordinate must be an upper letter
                if (letter < 'A' || letter > 'J') // A letter between A - J
                    correct = false;
                string num = coord[1].ToString().ToUpper(); // For second char, we need a string to convert a number into int
                if (num != "X") // X is valid to represent 10
                {
                    if (!Int32.TryParse(num, out int n) || (n < 1 || n > 9)) // Number between 1 - 9
                        correct = false;
                }
            }

            return correct;
        }

        public bool AddShip(string coord, bool horizontal)
        {
            bool correct = true;
            int x = Convert.ToInt32(Char.ToUpper(coord[0])) - 65; // Coordinate for rows, letter A (65) will be coord 0
            int y = (Char.ToUpper(coord[1]) == 'X' ? 10 : Convert.ToInt32(coord[1].ToString())) - 1; // Coordinate for columns, board length is 0-9, we need subtract 1
            int size = SHIP_SIZES[ships.Count]; // Depending on the length of our list of ships, we will add different sizes of ships
            List<Coordinates> coords = new();

            if (horizontal) // Horizontal direction
            {
                if ((y + size) > BOARD_SIZE) // We check if the size of ship added to his first coord will exceed the board
                    y -= (y + size) - BOARD_SIZE; // If exceed, we move the position of the ship until it fits on board

                for (int i = 0; i < size; i++) // We fill the list of coordinates
                    coords.Add(new Coordinates(x, y + i));
            }
            else // Vertical direction
            {
                if ((x + size) > BOARD_SIZE) // Same as horizontal direction but checking x coordinate
                    x -= (x + size) - BOARD_SIZE;

                for (int i = 0; i < size; i++) // We fill the list of coordinates
                    coords.Add(new Coordinates(x + i, y));
            }
            
            // We check if one coordinate is busy (if a 'S' is on a coordinate)
            bool busyCoord = false;
            for (int i = 0; i < coords.Count && busyCoord == false; i++)
            {
                if (board[coords[i].x, coords[i].y] == (char)Characters.Ship)
                {
                    busyCoord = true;
                    correct = false;
                }
            }

            if (!busyCoord)
            {
                // We add an 'S' to the board where the ship is placed
                foreach (Coordinates c in coords)
                    board[c.x, c.y] = (char)Characters.Ship;

                ships.Add(new Ship(size, coords)); // Add the ship to the list
            }

            return correct;
        }

        public bool CheckAttackedCoords(string coord) // Method that check if a coordinate has been attacked before
        {
            bool attacked = false;
            int x = Convert.ToInt32(Char.ToUpper(coord[0])) - 65; // Coordinate for rows, letter A (65) will be coord 0
            int y = (Char.ToUpper(coord[1]) == 'X' ? 10 : Convert.ToInt32(coord[1].ToString())) - 1; // Coordinate for columns, board length is 0-9, we need subtract 1

            if (board[x, y] == (char)Characters.Missed || board[x, y] == (char)Characters.Touched
                || board[x, y] == (char)Characters.Destroyed)
                attacked = true;

            return attacked;
        }

        public bool Attack(string coord)
        {
            if(!isEnemy)
                Console.Write($"El ENEMIGO ha atacado la coordenada {coord.ToUpper()}: ");
            else
                Console.Write($"Estás ATACANDO la coordenada {coord.ToUpper()}: ");

            bool success = false;
            int x = Convert.ToInt32(Char.ToUpper(coord[0])) - 65; // Coordinate for rows, letter A (65) will be coord 0
            int y = (Char.ToUpper(coord[1]) == 'X' ? 10 : Convert.ToInt32(coord[1].ToString())) - 1; // Coordinate for columns, board length is 0-9, we need subtract 1

            if (board[x, y] == (char)Characters.Ship) // If coordinate is 'S', the attack is successful
            {
                success = true;
                Ship s = new Ship();
                bool finded = false;
                for(int i = 0; i < ships.Count && !finded; i++) // We look for the damaged ship
                {
                    foreach(Coordinates c in ships[i].GetCoordinates())
                    {
                        if(c.x == x && c.y == y)
                        {
                            s = ships[i];
                            finded = true;
                        }
                    }
                }

                s.Lifes--; // Lifes from the ship reduced by 1

                if(s.Lifes == 0) // Ship destroyed
                {
                    foreach (Coordinates c in s.GetCoordinates())
                        board[c.x, c.y] = (char)Characters.Destroyed;
                    ships.Remove(s);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Barco destruido");
                }
                else // Ship damaged
                {
                    board[x, y] = (char)Characters.Touched;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Barco tocado");
                }
            }
            else // Failed attack
            {
                board[x, y] = (char)Characters.Missed;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Agua");
            }

            Console.ForegroundColor = ConsoleColor.White;
            return success;
        }
    }
}
