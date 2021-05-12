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
        public byte turn { get; private set; } = 0;
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
        private void Сastling(Figure king, bool isShortCastling)
        {
            if (isShortCastling)
            {
                Attack(king, _board[king.GetPos().X + 2, king.GetPos().Y]);
                Attack(_board[7, king.GetPos().Y], _board[king.GetPos().X + 1, king.GetPos().Y]);
            }
            else
            {
                Attack(king, _board[king.GetPos().X - 2, king.GetPos().Y]);
                Attack(_board[0, king.GetPos().Y], _board[king.GetPos().X - 1, king.GetPos().Y]);
            }

        }
        private void Attack(Figure f, Figure s)
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
        public void MakeAMove(int fX, int fY, int sX, int sY)
        {
            Figure f = _board[fX, fY];
            
            if (f._type == TypeFigure.King && fY == sY && Math.Abs(fX - sX) == 2)
            {
                if (sX > fX)
                    Сastling(f, true);
                else
                    Сastling(f, false);
            }
            else
            {
                Figure s = _board[sX, sY];
                Attack(f, s);
            }
        }
        private List<Point> FindPossibleMovesInDirection(Figure f, Direction direction, Figure[,] mas)
        {
            List<Point> possibleMoves = new List<Point>();
            Point p = f.GetPos();
            switch (direction)
            {
                case Direction.down:
                    for (int i = p.Y + 1; i < 8; i++)
                    {
                        if (mas[p.X, i]._team == 2)
                        {
                            possibleMoves.Add(new Point(p.X, i));
                            continue;
                        }
                        if (f._team != mas[p.X, i]._team)
                            possibleMoves.Add(new Point(p.X, i));
                        break;
                    }
                    break;
                case Direction.up:
                    for (int i = p.Y - 1; i >= 0; i--)
                    {
                        if (mas[p.X, i]._team == 2)
                        {
                            possibleMoves.Add(new Point(p.X, i));
                            continue;
                        }
                        if (f._team != mas[p.X, i]._team)
                            possibleMoves.Add(new Point(p.X, i));
                        break;
                    }
                    break;
                case Direction.right:
                    for (int i = p.X + 1; i < 8; i++)
                    {
                        if (mas[i, p.Y]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, p.Y));
                            continue;
                        }
                        if (f._team != mas[i, p.Y]._team)
                            possibleMoves.Add(new Point(i, p.Y));
                        break;
                    }
                    break;
                case Direction.left:
                    for (int i = p.X - 1; i >= 0; i--)
                    {
                        if (mas[i, p.Y]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, p.Y));
                            continue;
                        }
                        if (f._team != mas[i, p.Y]._team)
                            possibleMoves.Add(new Point(i, p.Y));
                        break;
                    }
                    break;
                case Direction.downRight:
                    for (int i = p.X + 1, j = p.Y + 1; i < 8 && j < 8; i++, j++)
                    {
                        if (mas[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != mas[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.upLeft:
                    for (int i = p.X - 1, j = p.Y - 1; i >= 0 && j >= 0; i--, j--)
                    {
                        if (mas[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != mas[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.downLeft:
                    for (int i = p.X - 1, j = p.Y + 1; i >= 0 && j < 8; i--, j++)
                    {
                        if (mas[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != mas[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
                case Direction.upRight:
                    for (int i = p.X + 1, j = p.Y - 1; i < 8 && j >= 0; i++, j--)
                    {
                        if (mas[i, j]._team == 2)
                        {
                            possibleMoves.Add(new Point(i, j));
                            continue;
                        }
                        if (f._team != mas[i, j]._team)
                            possibleMoves.Add(new Point(i, j));
                        break;
                    }
                    break;
            }
            return possibleMoves;
        }
        public List<Point> FindPossibleMoves(Figure f, Figure[,] mas, bool ignorCastling = false)
        {
            List<Point> temp = new List<Point>();
            IEnumerable<Point> possibleMoves = new List<Point>();
            Point p = f.GetPos();
            switch (f._type)
            {
                case TypeFigure.Pawn:
                    int mult = f._team == 0 ? -1 : 1;
                    if (mult + p.Y >= 0 && mult + p.Y < 8 && mas[p.X, mult + p.Y]._type == TypeFigure.EmptyCell)
                    {
                        temp.Add(new Point(p.X, mult + p.Y));
                        if (mas[p.X, mult * 2 + p.Y]._type == TypeFigure.EmptyCell && f._moveCounter == 0)
                            temp.Add(new Point(p.X, mult * 2 + p.Y));
                    }
                    for (int i = p.X - 1; i <= p.X + 1; i += 2)
                    {
                        if (i >= 0 && i < 8 && mas[i, mult + p.Y]._team < 2 && mas[i, mult + p.Y]._team != f._team)
                            temp.Add(new Point(i, mult + p.Y));
                    }

                    possibleMoves = temp;
                    break;
                case TypeFigure.Knight:
                    for (int i = -2; i <= 2; i += 4)
                    {
                        for (int j = -1; j <= 1; j += 2)
                        {
                            if (p.X + i >= 0 && p.X + i < 8 && p.Y + j >= 0 && p.Y + j < 8 &&
                                mas[p.X + i, p.Y + j]._team != f._team)
                                temp.Add(new Point(p.X + i, p.Y + j));
                            if (p.X + j >= 0 && p.X + j < 8 && p.Y + i >= 0 && p.Y + i < 8 &&
                                mas[p.X + j, p.Y + i]._team != f._team)
                                temp.Add(new Point(p.X + j, p.Y + i));
                        }
                    }
                    possibleMoves = temp;
                    break;
                case TypeFigure.Bishop:
                    for (int i = 4; i < 8; i++)
                        possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, (Direction)i, mas));
                    break;
                case TypeFigure.Rook:
                    for (int i = 0; i < 4; i++)
                        possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, (Direction)i, mas));
                    break;
                case TypeFigure.Queen:
                    for (int i = 0; i < 8; i++)
                        possibleMoves = possibleMoves.Concat(FindPossibleMovesInDirection(f, (Direction)i, mas));
                    break;
                case TypeFigure.King:

                    for (int i = p.X - 1; i < p.X + 2; i++)
                    {
                        for (int j = p.Y - 1; j < p.Y + 2; j++)
                        {
                            if ((i == p.Y && j == p.X) || (i < 0 || i > 7) || (j < 0 || j > 7) || this[i, j]._team == f._team)
                                continue;
                            temp.Add(new Point(i, j));
                        }
                    }
                    if (f._moveCounter == 0 && !ignorCastling)
                    {
                        if (СastlingCheck(this[0, p.Y])) temp.Add(new Point(p.X - 2, p.Y));
                        if (СastlingCheck(this[7, p.Y])) temp.Add(new Point(p.X + 2, p.Y));
                    }
                    possibleMoves = temp;
                    break;
            }
            return possibleMoves.ToList();
        }

        private void Copy(Figure[,] a, Figure[,] b)
        {
            for (int i = 0; i < a.Length / a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                {
                    b[i, j] = new Figure(a[i, j]);
                }
            }
        }
        private bool NotСheckAfterMove(Figure f, Figure s)
        {
            Figure[,] clone = new Figure[8,8];
            Copy(_board, clone);
            int fx = f.GetPos().X,
                fy = f.GetPos().Y,
                sx = s.GetPos().X,
                sy = s.GetPos().Y;
            Point kingPos = new Point(-1,-1);
            Attack(clone[fx, fy], clone[sx, sy]);
            foreach (Figure item in clone)
            {
                if (item._team == turn && item._type == TypeFigure.King)
                    kingPos = item.GetPos();
            }
            foreach (Figure item in clone)
            {
                if (item._type == TypeFigure.EmptyCell || item._team == turn)
                    continue;
                List<Point> posMoves = FindPossibleMoves(item, clone, true);
                if (posMoves.Contains(kingPos))
                    return false;
            }
            return true;
        }
        private bool СastlingCheck(Figure rook)
        {
            if (rook.GetPos().X == 7)
            {
                for (int i = 6; _board[i, rook.GetPos().Y]._type != TypeFigure.King; i--)
                {
                    if (_board[i, rook.GetPos().Y]._type != TypeFigure.EmptyCell)
                    {
                        return false;
                    }
                }
                for (int i = 4; i < 8; i++)
                {
                    Point posToCheck = new Point(i, rook.GetPos().Y);

                    foreach (Figure item in _board)
                    {
                        if (item._type == TypeFigure.EmptyCell || item._team == turn)
                            continue;
                        List<Point> posMoves = FindPossibleMoves(item, _board);
                        if (posMoves.Contains(posToCheck))
                            return false;
                    }
                }
            }
            else
            {
                for (int i = 1; _board[i, rook.GetPos().Y]._type != TypeFigure.King; i++)
                {
                    if (_board[i, rook.GetPos().Y]._type != TypeFigure.EmptyCell)
                    {
                        return false;
                    }
                }
                for (int i = 4; i >= 0; i--)
                {
                    Point posToCheck = new Point(i, rook.GetPos().Y);

                    foreach (Figure item in _board)
                    {
                        if (item._type == TypeFigure.EmptyCell || item._team == turn)
                            continue;
                        List<Point> posMoves = FindPossibleMoves(item, _board);
                        if (posMoves.Contains(posToCheck))
                            return false;
                    }
                }
            }

            return true;
        }
        public bool IsCorrectMove(int fX, int fY, int sX, int sY)
        {
            Figure f = _board[fX, fY]; 
            Figure s = _board[sX, sY];
            if (f._type != TypeFigure.EmptyCell && f._team == turn)
            {
                List<Point> moves = FindPossibleMoves(f, _board);
                if (moves.Contains(s.GetPos()) && NotСheckAfterMove(f, s))
                {
                    turn = (byte)(turn == 0 ? 1 : 0);
                    return true;
                }
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
