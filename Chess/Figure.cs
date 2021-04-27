using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum TypeFigure
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
        EmptyCell
    } 

    /// <summary>
    /// Класс описывающий шахматную фигуру.
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Счетчик шагов.
        /// </summary>
        public int _moveCounter { get; }
        public Bitmap _picture { get; set; }
        /// <summary>
        /// Сторона.
        /// 0 - белая, 1 - черная, 2 - пустая клетка.
        /// </summary>
        public byte _team { get; set; }
        /// <summary>
        /// Позиция фигуры.
        /// </summary>
        /// private
        private Point _position { get; set; }
        /// <summary>
        /// Тип фигуры.
        /// </summary>
        public TypeFigure _type { get; set; }
        /// <summary>
        /// Создание экземпляра класса Figure.
        /// </summary>
        /// <param name="team">Сторона.</param>
        /// <param name="type">Тип фигуры.</param>
        /// <param name="position">Позиция фигуры.</param>
        public Figure(byte team,TypeFigure type, Point position)
        {
            this._team = team;
            this._type = type;
            this._position = position;
            this._picture = GetPictureOrNullForEmptyCell(type, team);
        }
        /// <summary>
        /// Создание экземпляра класса Figure.
        /// </summary>
        /// <param name="team">Сторона.</param>
        /// <param name="type">Тип фигуры.</param>
        /// <param name="x">Позиция фигуры по координате x.</param>
        /// <param name="y">Позиция фигуры по координате y.</param>
        public Figure(byte team, TypeFigure type, int x, int y) : this(team, type, new Point(x, y))
        {
        }

        public Figure(Figure f)
        {
            this._team = f._team;
            this._type = f._type;
            this._position = f._position;
            this._picture = this._picture = GetPictureOrNullForEmptyCell(this._type, this._team);
        }
        public Point GetPos()
        {
            return _position;
        }
        public void SetPos(Point p)
        {
            _position = p;
        }
        private Bitmap GetPictureOrNullForEmptyCell(TypeFigure type, byte team)
        {
            Image im;
            String prefix = team == 0 ? "W" : "B";
            switch (type)
            {
                case TypeFigure.Pawn:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "Pawn");
                    break;
                case TypeFigure.Knight:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "Knight");
                    break;
                case TypeFigure.Bishop:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "Bishop");
                    break;
                case TypeFigure.Rook:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "Rook");
                    break;
                case TypeFigure.Queen:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "Queen");
                    break;
                case TypeFigure.King:
                    im = (Image)Properties.Resources.ResourceManager.GetObject(prefix + "King");
                    break;
                default:
                    im = null;
                    break;
            }

            return im == null ? null: new Bitmap(im, 80, 80);
        }
        

        public void ConvertToCell()
        {
            _team = 2;
            _type = TypeFigure.EmptyCell;
            _picture = GetPictureOrNullForEmptyCell(_type,_team);

        }
        public void  Display(Point position, Graphics gr)
        {
            if(_type != TypeFigure.EmptyCell)
                gr.DrawImage(this._picture,position);
        }
    }
}
