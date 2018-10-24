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
            get
            {
                if (GameState == State.Won || GameState == State.Lost) return 0;
                if (Board != null) return Board.MinesRemaining;
                return (int)numberOfMines;
            }
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
            if (GameState == State.Created)
            {
                return;
            }
            Board.MarkCell(x, y);
        }
    }
}
