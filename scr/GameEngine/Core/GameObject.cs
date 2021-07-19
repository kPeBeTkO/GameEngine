using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameEngine.Render.Texture;
using GameEngine.Core.Collisions;

namespace GameEngine.Core
{
    public abstract class GameObject
    {
        private TextureHolder textureHolder;
        public bool Loaded;
        public Body Body;
        public bool Collidable;
        public string State;

        public virtual Texture GetTexture()
        {
            return textureHolder.GetTexture(State);
        }
    }
}
