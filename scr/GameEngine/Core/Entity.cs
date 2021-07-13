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

        }

        public abstract void Collide(Entity entity);
    }
}
