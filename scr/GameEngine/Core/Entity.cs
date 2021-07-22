using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public abstract class Entity : GameObject
    {
        public Vector Speed = new Vector(0, 0);
        public void Move()
        {
            Body.Location += Speed;
        }

        public abstract void Collide(Entity entity);
    }
}
