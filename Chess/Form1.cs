using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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
        private ChessEngine engine;
        string moves;
        public Chess()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _board = new ChessBoard( 90, 90) ;
            engine = new ChessEngine();
        }

        private void Chess_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            _board.ChessBoardDisplay(gr);
        }

        private void Chess_MouseDown(object sender, MouseEventArgs e)
        {
            int cellX = (int)(Math.Floor((e.X - _boardX) / (double)80));
            int cellY = (int)(Math.Floor((e.Y - _boardY) / (double)80));
            if ((cellX >= 0 && cellX < 8) && (cellY >= 0 && cellY < 8))
            {
                _inDrag = true;
                _clickDownX = cellX;
                _clickDownY = cellY;
                _board.MarkPosibleMoves(_clickDownX, _clickDownY, this.CreateGraphics());
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

                if (_board.IsCorrectMove(_clickDownX, _clickDownY, _clickUpX, _clickUpY))
                {
                    moves += " ";
                    if (_board.MakeAMove(_clickDownX, _clickDownY, _clickUpX, _clickUpY) == TypeFigure.EmptyCell)
                    {
                        moves += NotationConverter.ConvertToUCI(_clickDownX, _clickDownY, _clickUpX, _clickUpY);
                    }
                    else
                    {
                        moves = NotationConverter.ConvertToUCI(_clickDownX, _clickDownY, _clickUpX, _clickUpY, TypeFigure.Queen, _board[_clickUpX, _clickUpY]._team);
                    }
                    
                    label1.Text = moves;
                    if (engine.isStarted)
                        GetMoveFromEngine();
                }
            }
            _inDrag = false;
            this.Invalidate();
        }

        private void Chess_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine.Dispose();
        }

        private void button_PlayWithEngine_Click(object sender, EventArgs e)
        {
            button_PlayWithEngine.Enabled = false;
            engine.Start();
            engine.RunCommand("uci");
            engine.RunCommand("setoption name Skill Level value 2");
            engine.RunCommand("setoption name UCI_LimitStrength value true");
            engine.RunCommand("isready");
            engine.RunCommand("ucinewgame");
        }
        async private void GetMoveFromEngine()
        {
            await Task.Run(() => engine.RunCommand("position startpos moves" + moves));
            string res = await Task.Run(() => engine.MakeMove("go movetime 1000"));
            int[] moveIndexes = NotationConverter.ConvertFromUCI(res);
            if (_board.IsCorrectMove(moveIndexes))
            { 
                _board.MakeAMove(moveIndexes);
                moves += " " + res;
                label1.Text = moves;
                this.Invalidate();
            }
        }

    }
}
