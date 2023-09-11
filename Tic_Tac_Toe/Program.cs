using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kółko_i_Krzyżyk
{
    internal class Program
    {
        public static string[,] board = new string[3, 3];
        static void Main(string[] args)
        {
            int x, y;
            int counter = 0;
            bool gameOver = false;
            string xo = "X";
            
            clearBoard();
            do
            {
                Console.WriteLine("Podaj współrzędne (x,y): ");

                bool intX = int.TryParse(Console.ReadLine(), out x);
                bool intY = int.TryParse(Console.ReadLine(), out y);

                if (!intX || !intY) x = -1;

                if ((x >= 0) && (x <= 2) && (y >= 0) && (y <= 2))
                {
                    if (board[x, y] == " ")
                    {
                        board[x, y] = xo;
                        counter++;

                        if (checkWin(xo))
                        {
                            printBoard();
                            gameOver = true;
                            Console.WriteLine("Gratulację wygrał gracz: " + xo);
                        }
                        else
                        {
                            switch (xo)
                            {
                                case "X":
                                    xo = "O";
                                    break;

                                case "O":
                                    xo = "X";
                                    break;
                            }

                            printBoard();

                        }

                    }
                    else
                    {
                        Console.WriteLine("To pole jest już zajęte");
                    }
                    
                } else
                {
                    Console.WriteLine("Złe współrzędne");
                }
                if ((counter == 9) && (gameOver == false)){
                    gameOver = true;
                    Console.WriteLine("Remis");
                }
            } while (!gameOver);

            Console.ReadKey();
        }

        static void clearBoard()
        {
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = " ";
                }
            }
        }

        static void printBoard()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            
        }

        static bool checkWin(string xo)
        {
            for(int i = 0; i < 3; i++)
            {
                bool w = true;
                for(int j = 0; j < 3; j++)
                {
                    if (board[i, j] != xo)
                    {
                        w = false;
                        break;
                    }
                }
                if (w)
                    return true;
            }
            for (int i = 0; i < 3; i++)
            {
                bool w = true;
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] != xo)
                    {
                        w = false;
                        break;
                    }
                }
                if (w)
                    return true;
            }
            if ((board[0, 0] == xo) && (board[1, 1] == xo) && board[2, 2] == xo) return true;
            if ((board[2, 0] == xo) && (board[1, 1] == xo) && board[0, 2] == xo) return true;
            
            return false;
        }
    }
}
