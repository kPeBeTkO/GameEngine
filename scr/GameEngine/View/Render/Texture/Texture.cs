using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngine.View.Render.Texture
{
    public class Texture
    {
        public readonly Bitmap Image;
        public readonly int Duration;
        public readonly float Height;
        public readonly float Width;

        public Texture(Bitmap image, double width, double height, int duration = 1)
        {
            Image = image;
            Height = (float)height;
            Width = (float)width;
            Duration = duration;
        }
    }
}
