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
    public class ChessBoard
    {
        private Figure[,] _board;
        private int _x;
        private int _y;

        public ChessBoard(int x = 0, int y = 0)
        {
            this._x = x;
            this._y = y;
            this._board = new Figure[8, 8];
            ToStartPos();
        }
        public void ToStartPos()
        {
            for (int i = 0; i < 8; i++)
            {
                    this[1, i] = new Figure(1, TypeFigure.Pawn, 1, i);
                    this[6, i] = new Figure(0, TypeFigure.Pawn, 6, i);
                for (int j = 2; j < 6; j++)
                {
                    this[j, i] = new Figure(2, TypeFigure.EmptyCell, j, i);
                }
            }
            this[0, 0] = new Figure(1, TypeFigure.Rook, 0, 0);
            this[0, 7] = new Figure(1, TypeFigure.Rook, 0, 7);
            this[7, 0] = new Figure(0, TypeFigure.Rook, 7, 0);
            this[7, 7] = new Figure(0, TypeFigure.Rook, 7, 7);

            this[0, 1] = new Figure(1, TypeFigure.Knight, 0, 1);
            this[0, 6] = new Figure(1, TypeFigure.Knight, 0, 6);
            this[7, 1] = new Figure(0, TypeFigure.Knight, 7, 1);
            this[7, 6] = new Figure(0, TypeFigure.Knight, 7, 6);

            this[0, 2] = new Figure(1, TypeFigure.Bishop, 0, 2);
            this[0, 5] = new Figure(1, TypeFigure.Bishop, 0, 5);
            this[7, 2] = new Figure(0, TypeFigure.Bishop, 7, 2);
            this[7, 5] = new Figure(0, TypeFigure.Bishop, 7, 5);

            this[0, 4] = new Figure(1, TypeFigure.King, 0, 4);
            this[7, 4] = new Figure(0, TypeFigure.King, 7, 4);

            this[0, 3] = new Figure(1, TypeFigure.Queen, 0, 3);
            this[7, 3] = new Figure(0, TypeFigure.Queen, 7, 3);
        }
        public void ChessBoardDisplay(Graphics gr)
        {
            Pen field = new Pen(Color.DarkCyan, 40);
            SolidBrush blackCells = new SolidBrush(Color.Gray);
            SolidBrush whiteCells = new SolidBrush(Color.NavajoWhite);

            gr.DrawRectangle(field, _x, _y, 640, 640);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((j + i) % 2 == 0)
                        gr.FillRectangle(whiteCells, _x + j * 80, _y + i * 80, 80, 80);
                    else
                        gr.FillRectangle(blackCells, _x + j * 80, _y + i * 80, 80, 80);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this[i, j].Display(new Point( _x + this[i, j].GetPos().Y * 80, _y + this[i, j].GetPos().X * 80), gr);
                }
            }

        }

        public void Attack(Figure f, Figure s)
        {
            s._type = f._type;
            s._team = f._team;
            s._picture = f._picture;
            f.ConvertToCell();

        }
        public bool IsCorrectMove(Figure first, Figure second)
        {
            if (first._team != second._team)
            {
                int fx = first.GetPos().X,
                fy = first.GetPos().Y,
                sx = second.GetPos().X,
                sy = second.GetPos().Y;
                switch (first._type)
                {
                    case TypeFigure.Pawn:
                        return false;
                    case TypeFigure.Knight:

                        if (Math.Abs(fx - sx) == 2 && Math.Abs(fy - sy) == 1 ||
                            Math.Abs(fx - sx) == 1 && Math.Abs(fy - sy) == 2 )
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case TypeFigure.Bishop:
                        return false;
                    case TypeFigure.Rook:
                        return false;
                    case TypeFigure.Queen:
                        return false;
                    case TypeFigure.King:
                        return false;
                }
            }
            return false;
        }
        
        public Figure this[int y, int x]
        {
            get
            {
                return _board[y, x];
            }
            set
            {
                _board[y, x] = value;
            }
        }

    }
}
