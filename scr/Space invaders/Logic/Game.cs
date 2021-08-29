using GameEngine.Logic;
using GameEngine.Logic.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Logic
{
    class Game : IUpdatable
    {
        public bool[,] Field;
        private Random random = new Random();
        private int ticksPassed;
        public int Score;
        private int ticksPerMove = 20;
        private int score = 0;
        public readonly int Width;
        public readonly int Height;
        public int Lives = 3;
        private GameObject player;

        public Game()
        {
            player = new Spaceship(this, 50, 15);
            Core.AddObject(player);
            Core.AddObject(new Wall(new Vector(50, 100), new Vector(-25, 50)));
            Core.AddObject(new Wall(new Vector(50, 100), new Vector(125, 50)));
            Core.AddObject(new Wall(new Vector(100, 50), new Vector(50, 125)));
            Core.AddObject(new Wall(new Vector(100, 50), new Vector(50, -25)));
            Core.AddObject(new Deadline(this));
        }

        public void Update()
        {
            ticksPassed++;
            foreach (var obj in Core.Objects)
                if (obj is Enemy)
                    ((Enemy)obj).TryShot();
            if (ticksPassed % 40 == 0)
            {
                GenerateEnemy(new Vector (0, -1));
            }
            if (Lives == 0)
                player.Dead = true;

        }

        void GenerateEnemy(Vector speed)
        {
            Core.AddObject(new Enemy(this, speed));
        }
    }
}
