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
            this.Invalidate();
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
        
        private void Chess_MouseUp(object sender, MouseEventArgs e)
        {
            int cellX = (int)(Math.Floor((e.X - _boardX) / (double)80));
            int cellY = (int)(Math.Floor((e.Y - _boardY) / (double)80));
            if ((cellX >= 0 && cellX <= 7) && (cellY >= 0 && cellY <= 7) && _inDrag)
            {
                _clickUpX = cellX;
                _clickUpY = cellY;
                label3.Text =  "  _clickDownY = " + _clickDownY.ToString() +" _clickDownX = " + _clickDownX.ToString();
                label3.Text += "\n_clickUpY = " + _clickUpY.ToString() + " _clickUpX = " + _clickUpX.ToString();
                label2.Text = cellX.ToString() + " " + cellY.ToString();
                

                if (_board.IsCorrectMove(_clickDownX,_clickDownY, _clickUpX, _clickUpY))
                {
                    _board.MakeAMove(_clickDownX, _clickDownY, _clickUpX, _clickUpY);
                }
                label4.Text = _board.turn == 0?"Белые": "Черные";
            }
            _inDrag = false;
        }
    }
}
