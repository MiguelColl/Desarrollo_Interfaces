using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HundirLaFlota
{
    internal class Game
    {
        private Board myBoard = new Board(false);
        private Board enemyBoard = new Board(true);
        private static Random rd = new Random();
        private static ILog log = Logs.GetLogger();

        public Game() { }

        public void Launch()
        {
            PlaceShips();
            PlaceEnemyShips();
            bool end = false;
            bool myTurn = true;
            bool successfulAttack;
            do
            {
                ShowBoard();
                string coord = SelectCoord(myTurn); // Select a coordinate for attack
                
                if (myTurn) // Player turn
                {
                    successfulAttack = enemyBoard.Attack(coord);
                }
                else // Enemy turn
                {
                    successfulAttack = myBoard.Attack(coord);
                }

                if (successfulAttack) // Successful attack, we need to check if game is over
                {
                    if (myBoard.GetShipsLength() == 0 || enemyBoard.GetShipsLength() == 0)
                        end = true;
                }
                else // If we don't attack successfully, change turn
                    myTurn = !myTurn;

                if (end)
                    ChangeTurn("Comprobando");
                else if (successfulAttack)
                {
                    if(myTurn)
                        ChangeTurn("Te vuelve a tocar");
                    else
                        ChangeTurn("Le vuelve a tocar");
                }
                else
                    ChangeTurn("Cambiando de turno");
            } while (!end);

            Console.Clear();
            ShowBoard();

            if (myTurn)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¡Enhorabuena! Has GANADO.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡Lástima! La maquina ha podido contigo, mas suerte la próxima vez.");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PlaceShips() // Method for place player's ships
        {
            string message = "";
            do
            {
                myBoard.Draw();
                if (message != "")
                {
                    Console.WriteLine(message);
                    message = "";
                }
                //Console.Write($"Coordenada para el {myBoard.GetShipsLength() + 1}º barco: ");
                Console.Write(string.Format(Program.rm.GetString("ask_coord"), myBoard.GetShipsLength() + 1));
                string coord = Console.ReadLine();
                if (coord != "")
                {
                    if (myBoard.CheckValidCoord(coord))
                    {
                        bool error;
                        int answer;
                        do
                        {
                            error = false;
                            Console.WriteLine("1. Horizontal");
                            Console.WriteLine("2. Vertical");
                            //Console.Write("¿Que orientación quieres? ");
                            Console.Write(Program.rm.GetString("ask_direction"));
                            if (!Int32.TryParse(Console.ReadLine(), out answer) || (answer < 1 || answer > 2))
                                error = true;
                        } while (error);

                        bool horizontal = answer == 1 ? true : false;
                        if (myBoard.AddShip(coord, horizontal))
                        {
                            message = "Barco colocado";
                            log.Info(message);
                        }
                        else
                            message = "Esas coordenadas coinciden con otro barco";
                    }
                    else
                        message = "Coordenada invalida";
                }
                Console.Clear();
            } while (myBoard.GetShipsLength() < myBoard.GetShipSizes());
        }

        public void PlaceEnemyShips() // Method for place ships on random coordinates
        {
            do
            {
                string coord = PickRandomCoord(); // We pick a random coordinate
                int direction = rd.Next(0, 2); // We pick a random direction 0 (false) - 1(true)
                bool dir = direction == 0 ? false : true;

                enemyBoard.AddShip(coord, dir); // If the coordinate is busy, ship will not be placed
            } while (enemyBoard.GetShipsLength() < enemyBoard.GetShipSizes());
            log.Info("Barcos enemigos colocados");
        }

        public string PickRandomCoord()
        {
            int letter = rd.Next(65, 75); // We pick a random letter between A - J
            int num = rd.Next(1, 11); // We pick a random number between 1 - 10
            string numString;
            if (num == 10)
                numString = "X";
            else
                numString = num.ToString();
            
            string coord = Convert.ToChar(letter).ToString() + numString;

            return coord;
        }

        public void ShowBoard()
        {
            ShowLeyend();
            enemyBoard.Draw();
            myBoard.Draw();
        }
        
        public void ShowLeyend()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write((char)Characters.Ship);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Barco  ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write((char)Characters.Missed);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Agua  ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write((char)Characters.Touched);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Tocado  ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)Characters.Destroyed);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Destruido  ");

            Console.WriteLine();
            Console.WriteLine();
        }

        public string SelectCoord(bool myTurn) // Method that return a coordinate that hasn't been attacked before
        {
            bool attackedCoord;
            string message = "";
            string coord;
            do // We choose a coordinate that hasn't been attacked before
            {
                if (message != "") // Message for errors
                {
                    ShowBoard();
                    Console.WriteLine(message);
                }

                coord = AskAttack(myTurn);

                if (myTurn) // Player turn
                {
                    attackedCoord = enemyBoard.CheckAttackedCoords(coord);
                }
                else // Enemy turn
                {
                    attackedCoord = myBoard.CheckAttackedCoords(coord);
                }

                if (attackedCoord)
                {
                    if (myTurn) // We dont show a message of error in the enemy turn
                    { 
                        message = "La coordenada " + coord.ToUpper() + " ya ha sido atacada";
                        Console.Clear();
                    }
                }
            } while (attackedCoord);

            return coord;
        }

        public string AskAttack(bool myturn) //Method that return a valid coordinate chosen by player or a random for enemies
        {
            string coordAttack = "";
            if (myturn) // Player turn
            {
                string message = "";
                do
                {
                    if (message != "") // Message for errors
                    {
                        Console.Clear();
                        ShowBoard();
                        Console.WriteLine(message);
                        message = "";
                    }
                    Console.Write("¿Qué coordenada quieres atacar? ");
                    string coord = Console.ReadLine();
                    if (coord != "")
                    {
                        if (myBoard.CheckValidCoord(coord))
                        {
                            coordAttack = coord;
                        }
                        else
                            message = "Coordenada invalida";
                    }
                } while (coordAttack == "");
            }
            else // Enemy turn
            {
                coordAttack = PickRandomCoord();
            }
            return coordAttack;
        }

        public void ChangeTurn(string text) // Method to stop the thread some seconds before change turn
        {
            Console.CursorVisible = false;
            Console.Write(text);
            for(int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
