using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Game
    {
        private uint width;
        private uint height;
        private uint numberOfMines;

        public Game(uint width, uint height, uint numberOfMines)
        {
            this.width = width;
            this.height = height;
            this.numberOfMines = numberOfMines;
            this.MinesRemaining = (int)numberOfMines;
            this.GameState = State.Created;
            this.StartedAt = null;
        }

        public Board Board
        {
            get;
            private set;
        }

        public DateTime? StartedAt
        {
            get;
            private set;
        }

        public int MinesRemaining
        {
            get;
            private set;
        }

        public enum State { Created, Running, Won, Lost};

        public State GameState
        {
            get;
            private set;
        }

        public void OpenCell(uint x, uint y)
        {
            if (GameState == State.Created)
            {
                this.Board = new Board(this.width, this.height, this.numberOfMines);
                GameState = State.Running;
                StartedAt = DateTime.Now;
            }

            if (this.Board.BoardArray[x, y].IsMine)
            {
                GameState = State.Lost;
                Board.OpenMines();
                return;
            }

            Board.OpenCell(x, y);

            if (Board.IsUserWon()) GameState = State.Won;
        }

        public void MarkCell(uint x, uint y)
        {
            if (Board.BoardArray[x, y].State == Cell.CellState.Opened) return;
            if (Board.BoardArray[x, y].IsMarked)
            {
                Board.BoardArray[x, y].IsMarked = false;
                this.MinesRemaining++;
            }
            else
            {
                Board.BoardArray[x, y].IsMarked = true;
                this.MinesRemaining--;
            }
        }
    }
}
