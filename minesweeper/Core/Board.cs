using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Board
    {
        private static double MaxMinesTheshold = 0.4;
        private static uint MaxBoardSize = 30;

        public Cell[,] BoardArray;

        public Board(uint width, uint height, uint numberOfMines)
        {
            if (width == 0 || height == 0) throw new Exception("Width and height must be more than zero");
            if (width > MaxBoardSize || height > MaxBoardSize) throw new Exception("Width and height must be less than " + MaxBoardSize.ToString());
            if (numberOfMines > width * height * MaxMinesTheshold) throw new Exception("Number of mines must be less than " + (width * height * MaxMinesTheshold).ToString());

            this.BoardArray = new Cell[width, height];
            this.CreateRandomBoard(width, height, numberOfMines);
        }

        private void CreateRandomBoard(uint width, uint height, uint numberOfMines)
        {
            for(uint i = 0; i < width; i++)
            {
                for(uint j = 0; j < height; j++)
                {
                    this.BoardArray[i, j] = new Cell();
                }
            }

            Random r = new Random();

            while (numberOfMines != 0)
            {
                int x = r.Next(0, (int)width);
                int y = r.Next(0, (int)height);


                if (this.BoardArray[x, y].IsMine != true)
                {
                    this.BoardArray[x, y].IsMine = true;
                    numberOfMines--;
                }
            }

            for(uint i = 0; i < width; i++)
            {
                for(uint j = 0; j < height; j++)
                {
                    int mines = 0;

                    if (isCoordsValid(i - 1, j - 1) && this.BoardArray[i - 1, j - 1].IsMine) mines++;
                    if (isCoordsValid(i - 1, j) && this.BoardArray[i - 1, j].IsMine) mines++;
                    if (isCoordsValid(i - 1, j + 1) && this.BoardArray[i - 1, j + 1].IsMine) mines++;
                    if (isCoordsValid(i, j + 1) && this.BoardArray[i, j + 1].IsMine) mines++;
                    if (isCoordsValid(i + 1, j + 1) && this.BoardArray[i + 1, j + 1].IsMine) mines++;
                    if (isCoordsValid(i + 1, j) && this.BoardArray[i + 1, j].IsMine) mines++;
                    if (isCoordsValid(i + 1, j - 1) && this.BoardArray[i + 1, j - 1].IsMine) mines++;
                    if (isCoordsValid(i, j - 1) && this.BoardArray[i, j - 1].IsMine) mines++;

                    this.BoardArray[i, j].MinesAround = mines;
                }
            }
        }

        private bool isCoordsValid(uint x, uint y)
        {
            if (x < 0 || y < 0 || x >= this.BoardArray.GetLength(0) || y >= this.BoardArray.GetLength(1)) return false;
            return true;
        }

        public void OpenCell(uint x, uint y)
        {
            if (this.BoardArray[x, y].IsMine) return;

            if (this.BoardArray[x, y].State == Cell.CellState.Opened) return;

            this.BoardArray[x, y].State = Cell.CellState.Opened;

            if (this.BoardArray[x, y].MinesAround > 0) return;

            if (isCoordsValid(x - 1, y - 1)) this.OpenCell(x - 1, y - 1);
            if (isCoordsValid(x - 1, y)) this.OpenCell(x - 1, y);
            if (isCoordsValid(x - 1, y + 1)) this.OpenCell(x - 1, y + 1);
            if (isCoordsValid(x, y + 1)) this.OpenCell(x, y + 1);
            if (isCoordsValid(x + 1, y + 1)) this.OpenCell(x + 1, y + 1);
            if (isCoordsValid(x + 1, y)) this.OpenCell(x + 1, y);
            if (isCoordsValid(x + 1, y - 1)) this.OpenCell(x + 1, y - 1);
            if (isCoordsValid(x, y - 1)) this.OpenCell(x, y - 1);

        }
    }
}
