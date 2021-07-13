using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngine.Render.Texture
{
    public class Texture
    {
        public readonly Bitmap Image;
        public readonly int Duration;
        public readonly SizeF Size;

        public Texture(Bitmap image, SizeF size, int duration = 1)
        {
            Image = image;
            Size = size;
            Duration = duration;
        }
    }
}
