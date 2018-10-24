using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private uint _boardWidth = 10;
        private uint _boardHeight = 10;
        private uint _cellSize = 25;
        private uint _numberOfMines = 2;
        private Game Game { get; set; }
        private CellButton[,] Cells { get; set; } 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BoardPanel.Controls.Clear();
            this.Game = new Game(_boardWidth, _boardHeight, _numberOfMines);
            this.Cells = new CellButton[_boardHeight, _boardWidth];

            for (var i = 0; i < _boardHeight; i++)
            {
                for (var j = 0; j < _boardWidth; j++)
                {
                    var newButton = new CellButton();
                    this.Cells[i, j] = newButton;
                    newButton.Row = (uint)i;
                    newButton.Column = (uint)j;
                    newButton.Width = (int)_cellSize;
                    newButton.Height = (int)_cellSize;
                    newButton.Left = (int)_cellSize * j;
                    newButton.Top = i * (int)_cellSize;
                    newButton.MouseUp += new MouseEventHandler(this.dynamicBtn_Click);
                    this.BoardPanel.Controls.Add(newButton);
                }
            }

            this.RefreshBoard();
        }

        private void dynamicBtn_Click(object sender, MouseEventArgs e)
        {
            var button = (CellButton)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.Game.MarkCell(button.Row, button.Column);
            }
            else
            {
                this.Game.OpenCell(button.Row, button.Column);
            }
            this.RefreshBoard();
        }

        private void RefreshBoard()
        {
            label1.Text = Game.MinesRemaining.ToString();
            for(var i = 0; i < _boardHeight; i++)
            {
                for(var j = 0; j < _boardWidth; j++)
                {
                    if (Game.GameState == Game.State.Created)
                    {
                        Cells[i, j].Text = "";
                    }
                    else if (Game.GameState == Game.State.Lost)
                        {
                            Cells[i, j].Text = Game.Board.BoardArray[i, j].IsMine ?
                            "*" : 
                            (
                            Game.Board.BoardArray[i, j].MinesAround > 0 ?
                            Game.Board.BoardArray[i, j].MinesAround.ToString() : 
                            "");
                        }
                    else if (Game.GameState == Game.State.Won)
                    {
                        Cells[i, j].Text = Game.Board.BoardArray[i, j].IsMine ?
                           "m" :
                           (
                               Game.Board.BoardArray[i, j].MinesAround > 0 ?
                               Game.Board.BoardArray[i, j].MinesAround.ToString() :
                           "");
                    }
                    else if (Game.GameState == Game.State.Running)
                    {
                        if (Game.Board.BoardArray[i, j].State == Cell.CellState.Closed)
                        {
                            Cells[i, j].Text = Game.Board.BoardArray[i, j].IsMarked ? "m" : "";
                        }
                        else
                        {
                            Cells[i, j].Text = Game.Board.BoardArray[i, j].MinesAround > 0 ?
                            Game.Board.BoardArray[i, j].MinesAround.ToString() :
                            "o";
                        }
                    }

                }
            }
        }
    }
}
