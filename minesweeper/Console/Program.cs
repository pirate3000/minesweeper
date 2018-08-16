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
        static void Main(string[] args)
        {
            while (true)
            {
                Board b = new Board(10, 10, 50);
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if (b.BoardArray[i, j].IsMine)
                            Console.Write("*\t");
                        else if(b.BoardArray[i, j].MinesAround == 0) Console.Write(" \t");
                           else Console.Write(b.BoardArray[i, j].MinesAround + "\t"); 
                    }

                    Console.WriteLine();
                }

                Console.ReadLine();
            }
        }
    }
}
