using GameEngine.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Logic
{
    class Game : IUpdatable
    {
        public bool[,] Field;
        public Figure CurrentFigure;
        public TetrisFigure Next;
        private Random random = new Random();
        private int ticksPassed;
        private int ticksPerMove => Math.Max(15 - figuresPast / 5, 1);
        private int figuresPast;
        public readonly int Width;
        public readonly int Height;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            Field = new bool[width + 2, height + 6];
            for (int i = 0; i < width + 2; i++)
            { 
                Field[i, 0] = true;
                Core.AddObject(new Block(i, 0, Color.Gray, null){ Fixed = true });;
            }
            for (int i = 0; i < height + 1; i++)
            {
                Field[0, i] = true;
                Core.AddObject(new Block(0, i, Color.Gray, null){ Fixed = true});
                Field[width + 1, i] = true;
                Core.AddObject(new Block(width + 1, i, Color.Gray, null){ Fixed = true });
            }
            Next = (TetrisFigure)random.Next(7);
            GenerateFigure();
        }

        public void Update()
        {
            ticksPassed++;
            if (ticksPassed == ticksPerMove)
            {
                ticksPassed = 0;
                if (!CurrentFigure.MoveDown())
                {
                    var rows = CurrentFigure.GetRows();
                    CurrentFigure.Dispose();
                    TryClearRows(rows);
                    GenerateFigure();
                }
            }
            foreach(var key in Core.KeysPressed)
                if (key.Press)
                    switch(key.Key)
                    {
                        case "Up":
                            CurrentFigure.Rotate();
                            break;
                        case "Right":
                            CurrentFigure.MoveRight();
                            break;
                        case "Left":
                            CurrentFigure.MoveLeft();
                            break;
                        case "Down":
                            SkipDown();
                            break;
                    }
        }

        public void SkipDown()
        {
            ticksPassed = ticksPerMove - 1;
        }

        public void TryClearRows(int[] rows)
        {
            var cleared = new List<int>();
            var blocksToDelete = new List<GameObject>();
            foreach(var row in rows)
            {
                var full = true;
                for (var j = 0; j <= Width; j++)
                {
                    if (!Field[j, row])
                    {
                        full = false;
                        break;
                    }
                }
                if (full)
                {
                    cleared.Add(row);
                    foreach(var block in Core.Objects)
                    {
                        if (block is Block && !block.Fixed && block.Body.Location.Y == row)
                            blocksToDelete.Add(block);
                    }
                }
            }
            foreach(var block in blocksToDelete)
                block.Dead = true;
            for (var i = 1; i < Height + 6; i++)
                for (var j = 1; j < Width + 1; j++)
                {
                    Field[j, i] = false;
                }
            foreach(var block in Core.Objects.Where(o => o is Block && !o.Fixed && !o.Dead).Cast<Block>())
            {
                block.Body.Location += new Vector(0, -cleared.Count(i => i < block.Body.Location.Y));
                Field[(int)block.Body.Location.X, (int)block.Body.Location.Y] = true;
            }
        }

        void GenerateFigure()
        {
            figuresPast++;
            CurrentFigure = Figure.Create(Next, Width / 2, Height, Field);
            Next = (TetrisFigure)random.Next(7);
            Core.AddObjects(CurrentFigure.blocks);
        }
    }
}
