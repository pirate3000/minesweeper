using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace ConsoleApp
{
    class Program
    {
        static void PrintBoard(Board b)
        {
            for (int i = 0; i < b.BoardArray.GetLength(0); i++)
            {
                for (int j = 0; j < b.BoardArray.GetLength(1); j++)
                {
                    if (b.BoardArray[i, j].IsMine)
                        Console.Write("*(c)\t");
                   // else if (b.BoardArray[i, j].MinesAround == 0) Console.Write(" \t");
                    else Console.Write(b.BoardArray[i, j].MinesAround + (b.BoardArray[i, j].State == Cell.CellState.Opened?"(o)": "(c)") + "\t");
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Board b = new Board(10, 10, 30);
            Program.PrintBoard(b);
            b.OpenCell(0, 0);
            Console.WriteLine();
            PrintBoard(b);

           Console.ReadLine();
        }
    }
}
