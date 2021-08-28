using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameEngine.View.Render.Texture;
using GameEngine.Logic.Collisions;

namespace GameEngine.Logic
{
    public abstract class GameObject
    {
        protected TextureHolder textureHolder;
        public Body Body;
        public string State;

        public int DrawPriority;
        public bool Visible = true;

        public bool Solid;
        public bool Fixed;
        public bool Collidable;
        public bool Dead;

        public virtual Texture GetTexture()
        {
            return textureHolder.GetTexture(State);
        }

        public virtual void Collide(GameObject obj)
        {

        }
    }
}
