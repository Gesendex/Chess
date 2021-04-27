using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Chess : Form
    {
        private bool _inDrag = false;
        private int _boardX = 100, _boardY = 100;
        private ChessBoard _board;
        private int _clickDownX;
        private int _clickDownY;
        private int _clickUpX;
        private int _clickUpY;
        public Chess()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _board = new ChessBoard(90, 90);
        }

        private void Chess_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            _board.ChessBoardDisplay(gr);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Chess_MouseDown(object sender, MouseEventArgs e)
        {

            int cellX = (int)(Math.Floor((e.X - _boardX) / (double)80));
            int cellY = (int)(Math.Floor((e.Y - _boardY) / (double)80));
            if ((cellX >= 0 && cellX <= 8) && (cellY >= 0 && cellY <= 8))
            {
                _inDrag = true;
                _clickDownX = cellX;
                _clickDownY = cellY;
                label1.Text = cellX.ToString() + " " + cellY.ToString();
            }

        }
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        private void Chess_MouseUp(object sender, MouseEventArgs e)
        {
            int cellX = (int)(Math.Floor((e.X - _boardX) / (double)80));
            int cellY = (int)(Math.Floor((e.Y - _boardY) / (double)80));
            if ((cellX >= 0 && cellX <= 7) && (cellY >= 0 && cellY <= 7) && _inDrag)
            {
                _clickUpX = cellX;
                _clickUpY = cellY;
                label3.Text =  "  _clickFigureDownY = " + _clickDownY.ToString() +" _clickFigureDownX = " + _clickDownX.ToString();
                label3.Text += "\n_clickFigureUpY = " + _clickUpY.ToString() + " _clickFigureUpX = " + _clickUpX.ToString();
                label2.Text = cellX.ToString() + " " + cellY.ToString();

                if (_board.IsCorrectMove(_board[_clickDownY, _clickDownX], _board[_clickUpY, _clickUpX]))
                {
                    _board.Attack(_board[_clickDownY, _clickDownX], _board[_clickUpY, _clickUpX]);
                }

            }
            _inDrag = false;
        }
    }
}
