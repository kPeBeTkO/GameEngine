using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public abstract class Entity : GameObject
    {
        public Vector Speed;
        public void Move()
        {
            Body.Location += Speed;
        }

        public abstract void Collide(Entity entity);
    }
}
