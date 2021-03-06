using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngine.View.Render.Texture
{
    public class TextureHolder
    {
        readonly Dictionary<string, Texture[]> textures;
        int currentFrame;
        string previousState;
        int ticksPassed;

        public TextureHolder(Dictionary<string, Texture[]> _textures)
        {
            textures = _textures;
        }

        public Texture GetTexture(string state)
        {
            if (!textures.ContainsKey(state))
                throw new Exception("Unknown state");
            if (previousState != state)
            {
                currentFrame = 0;
                ticksPassed = 0;
                previousState = state;
            }
            if (ticksPassed >= textures[state][currentFrame].Duration)
            {
                ticksPassed = 0;
                currentFrame = (currentFrame + 1) % textures[state].Length;
            }
            ticksPassed++;
            return textures[state][currentFrame];
        }
    }
}
