﻿using System;
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
        private enum Direction
        {
            down,
            up,
            right,
            left,
            downRight,
            upLeft,
            downLeft,
            upRight
        }

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
                this[i, 1] = new Figure(1, TypeFigure.Pawn, i, 1);
                this[i, 6] = new Figure(0, TypeFigure.Pawn, i, 6);
                for (int j = 2; j < 6; j++)
                {
                    this[i, j] = new Figure(2, TypeFigure.EmptyCell, i, j);
                }
            }
            this[0, 0] = new Figure(1, TypeFigure.Rook, 0, 0);
            this[7, 0] = new Figure(1, TypeFigure.Rook, 7, 0);
            this[0, 7] = new Figure(0, TypeFigure.Rook, 0, 7);
            this[7, 7] = new Figure(0, TypeFigure.Rook, 7, 7);

            this[1, 0] = new Figure(1, TypeFigure.Knight, 1, 0);
            this[6, 0] = new Figure(1, TypeFigure.Knight, 6, 0);
            this[1, 7] = new Figure(0, TypeFigure.Knight, 1, 7);
            this[6, 7] = new Figure(0, TypeFigure.Knight, 6, 7);

            this[2, 0] = new Figure(1, TypeFigure.Bishop, 2, 0);
            this[5, 0] = new Figure(1, TypeFigure.Bishop, 5, 0);
            this[2, 7] = new Figure(0, TypeFigure.Bishop, 2, 7);
            this[5, 7] = new Figure(0, TypeFigure.Bishop, 5, 7);

            this[4, 0] = new Figure(1, TypeFigure.King, 4, 0);
            this[4, 7] = new Figure(0, TypeFigure.King, 4, 7);

            this[3, 0] = new Figure(1, TypeFigure.Queen, 3, 0);
            this[3, 7] = new Figure(0, TypeFigure.Queen, 3, 7);
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
                    this[i, j].Display(new Point((_x + this[i, j].GetPos().X * 80), (_y + this[i, j].GetPos().Y * 80)), gr);
                }
            }

        }

        public void Attack(Figure f, Figure s)
        {
            s._type = f._type;
            s._team = f._team;
            s._picture = f._picture;
            s._moveCounter = f._moveCounter + 1;
            if (s._type == TypeFigure.Pawn && (s.GetPos().Y == 7 || s.GetPos().Y == 0))
            {
                s.ConvertTo(TypeFigure.Queen, s._team);
            }
            f.ConvertTo(TypeFigure.EmptyCell);

        }
        private List<Point> FindPossibleMovesInDirection(Figure f, Direction direction)
        {
            List<Point> possibleMoves = new List<Point>();
            Point p = f.GetPos();
            switch (direction)
            {
                case Direction.down:
                    for (int i = p.Y + 1; i < 8; i++)
                    {
                        if (this[p.X, i]._team == 2)
                        {
                            possibleMoves.Add(new Point(p.X, i));
                            continue;
                        }
                        if (f._team != this[p.X, i]._team)
                            possibleMoves.Add(new Point(p.X, i));
                        break;
                    }
                    break;
                case Direction.up:
                    for (int i = p.Y - 1; i >= 0; i--)
                    {
                        if (this[p.X, i]._team == 2)
                        {
                            possibleMoves.Add(new Point(p.X, i));
                            continue;
                        }
                        if (f._team != this[p.X, i]._team)
                            possibleMoves.Add(new Point(p.X, i));
                        break;
                    }
                    break;
                case Direction.right:
                    for (int i = p.X + 1; i < 8; i++)
                    {
                        if (this[i, p.Y]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, p.Y));
                            continue;
                        }
                        if (f._team != this[i, p.Y]._team)
                            possibleMoves.Add(new Point(i, p.Y));
                        break;
                    }
                    break;
                case Direction.left:
                    for (int i = p.X - 1; i >= 0; i--)
                    {
                        if (this[i, p.Y]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, p.Y));
                            continue;
                        }
                        if (f._team != this[i, p.Y]._team)
                            possibleMoves.Add(new Point(i, p.Y));
                        break;
                    }
                    break;
                case Direction.downRight:
                    for (int i = p.X + 1, j = p.Y + 1; i < 8 && j < 8; i++, j++)
                    {
                        if (this[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != this[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.upLeft:
                    for (int i = p.X - 1, j = p.Y - 1; i >= 0 && j >= 0; i--, j--)
                    {
                        if (this[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != this[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.downLeft:
                    for (int i = p.X - 1, j = p.Y + 1; i >= 0 && j < 8; i--, j++)
                    {
                        if (this[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != this[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.upRight:
                    for (int i = p.X + 1, j = p.Y - 1; i < 8 && j >= 0; i++, j--)
                    {
                        if (this[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != this[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
            }
            return possibleMoves;
        }
        public List<Point> FindPossibleMoves(Figure f)
        {
            IEnumerable<Point> possibleMoves = new List<Point>(); ;
            Point p = f.GetPos();
            switch (f._type)
            {
                case TypeFigure.Pawn:
                    break;
                case TypeFigure.Knight:
                    break;
                case TypeFigure.Bishop:
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.downRight));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.downLeft));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.upRight));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.upLeft));
                    break;
                case TypeFigure.Rook:
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.down));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.up));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.left));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.right));
                    break;
                case TypeFigure.Queen:
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.downRight));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.downLeft));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.upRight));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.upLeft));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.down));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.up));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.left));
                    possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, Direction.right));
                    break;
                case TypeFigure.King:
                    List<Point> temp = new List<Point>();
                    for (int i = p.X - 1; i < p.X + 2; i++)
                    {
                        for (int j = p.Y - 1; j < p.Y + 2; j++)
                        {
                            if ((i == p.Y && j == p.X) || (i < 0 || i > 7) || (j < 0 || j > 7) ||this[i,j]._team == f._team)
                                continue;
                            temp.Add(new Point(i, j));
                        }
                    }
                    possibleMoves = temp;
                    break;
                case TypeFigure.EmptyCell:
                    break;
                default:
                    break;
            }
            return possibleMoves.ToList();
        }
        public bool IsCorrectMove(Figure f, Figure s)
        {
            if (f._team != s._team && f._type != TypeFigure.EmptyCell)
            {
                int fx = f.GetPos().X,
                    fy = f.GetPos().Y,
                    sx = s.GetPos().X,
                    sy = s.GetPos().Y;
                List<Point> moves;
                switch (f._type)
                {
                    case TypeFigure.Pawn:
                        if (sx == fx && s._type == TypeFigure.EmptyCell)
                        {
                            bool firstMove = (f._moveCounter == 0);
                            if (f._team == 0 && fy - sy == 1 || fy - sy == 2 && firstMove)
                                return true;
                            if (f._team == 1 && sy - fy == 1 || sy - fy == 2 && firstMove)
                                return true;
                            return false;
                        }
                        else if (Math.Abs(sx - fx) == 1 && s._type != TypeFigure.EmptyCell && s._team != f._team)
                        {
                            if (f._team == 0 && fy - sy == 1)
                                return true;
                            if (f._team == 1 && sy - fy == 1)
                                return true;
                            return false;
                        }
                        else
                        {
                            return false;
                        }
                    case TypeFigure.Knight:
                        if (Math.Abs(fx - sx) == 2 && Math.Abs(fy - sy) == 1 ||
                            Math.Abs(fx - sx) == 1 && Math.Abs(fy - sy) == 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }
                moves = FindPossibleMoves(f);
                if (moves.Contains(s.GetPos()))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public Figure this[int x, int y]
        {
            get
            {
                return _board[x, y];
            }
            set
            {
                _board[x, y] = value;
            }
        }

    }
}
