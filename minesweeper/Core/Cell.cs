using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Cell
    {
        public enum CellState { Opened, Closed};

        public int MinesAround = 0;
        public CellState State = CellState.Closed;
        public bool IsMine = false;
        public bool IsMarked = false;

    }
}
