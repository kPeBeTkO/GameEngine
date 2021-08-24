using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Logic;

namespace Tetris.Logic
{
    class Figure
    {
        
        public Block[] blocks;
        public TetrisFigure Type;
        private bool[,] field;
        private Figure(TetrisFigure type, bool[,] field)
        {
            this.field = field;
            Type = type;
        }

        public bool Rotate()
        {
            if (Type == TetrisFigure.O)
                return false;
            var newPos = new Vector[4];
            newPos[0] = blocks[0].Body.Location;
            for (var i = 1; i < 4; i++)
            {
                var offset = blocks[i].Body.Location - blocks[0].Body.Location;
                var newPoint = blocks[0].Body.Location + Vector.FromAngle(offset.Angle + Math.PI / 2, offset.Length);
                newPos[i] = new Vector(Math.Round(newPoint.X), Math.Round(newPoint.Y));
            }
            if (!TryPlace(newPos))
                return false;
            for (var i = 0; i < 4; i++)
            {
                blocks[i].Body.Location = newPos[i];
            }
            return true;
        }

        public bool MoveLeft()
        {
            return Move(new Vector(-1, 0));
        }

        public bool MoveRight()
        {
            return Move(new Vector(1, 0));
        }

        public bool MoveDown()
        {
            return Move(new Vector(0,-1));
        }

        private bool Move(Vector offset)
        {
            var newPos = new Vector[4];
            for (var i = 0; i < 4; i++)
            {
                newPos[i] = blocks[i].Body.Location + offset;
            }
            if (!TryPlace(newPos))
                return false;
            for (var i = 0; i < 4; i++)
            {
                blocks[i].Body.Location = newPos[i];
            }
            return true;
        }

        public bool TryPlace(Vector[] newPos)
        {
            foreach(var point in newPos)
            {
                if (field[(int)point.X, (int)point.Y])
                    return false;
            }
            return true;
        }

        public void Dispose()
        {
            foreach(var block in blocks)
            { 
                block.Figure = null;
                field[(int)block.Body.Location.X, (int)block.Body.Location.Y] = true;
            }
        }

        public int[] GetRows()
        {
            var rows = new HashSet<int>();
            foreach(var block in blocks)
                rows.Add((int)block.Body.Location.Y);
            return rows.ToArray();
        }

        static Dictionary<TetrisFigure, Color> Colors = new Dictionary<TetrisFigure, Color> 
        {
            [TetrisFigure.I] = Color.Cyan,
            [TetrisFigure.J] = Color.Blue,
            [TetrisFigure.L] = Color.Orange,
            [TetrisFigure.O] = Color.Yellow,
            [TetrisFigure.S] = Color.Green,
            [TetrisFigure.T] = Color.Purple,
            [TetrisFigure.Z] = Color.Red
        };

        public static Figure Create(TetrisFigure type, int x, int y, bool[,] field)
        {
            var figure = new Figure(type, field);
            var color = Colors[type];
            switch(type)
            {
                case TetrisFigure.I:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y, color, figure), 
                                                 new Block(x + 1, y, color, figure), 
                                                 new Block(x + 2, y, color, figure)};
                    break;
                case TetrisFigure.J:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y, color, figure), 
                                                 new Block(x - 1, y + 1, color, figure), 
                                                 new Block(x + 1, y, color, figure)};
                    break;
                case TetrisFigure.L:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y, color, figure), 
                                                 new Block(x + 1, y + 1, color, figure), 
                                                 new Block(x + 1, y, color, figure)};
                    break;
                case TetrisFigure.O:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x + 1, y, color, figure), 
                                                 new Block(x + 1, y + 1, color, figure), 
                                                 new Block(x, y + 1, color, figure)};
                    break;
                case TetrisFigure.S:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y, color, figure), 
                                                 new Block(x, y + 1, color, figure), 
                                                 new Block(x + 1, y + 1, color, figure)};
                    break;
                case TetrisFigure.T:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y, color, figure), 
                                                 new Block(x + 1, y, color, figure), 
                                                 new Block(x, y + 1, color, figure)};
                    break;
                case TetrisFigure.Z:
                    figure.blocks = new Block[] {new Block(x, y, color, figure), 
                                                 new Block(x - 1, y + 1, color, figure), 
                                                 new Block(x, y + 1, color, figure), 
                                                 new Block(x + 1, y, color, figure)};
                    break;
            }
            return figure;
        }
    }
}
