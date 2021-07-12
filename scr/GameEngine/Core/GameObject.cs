using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameEngine.Core.Texture;

namespace GameEngine.Core
{
    public abstract class GameObject
    {
        private TextureHolder textureHolder;
        public bool Loaded;
        public Body Body;
        public bool Collidable;
        public string State;

        public Bitmap GetTexture()
        {
            return textureHolder.GetTexture(State);
        }
    }
}
