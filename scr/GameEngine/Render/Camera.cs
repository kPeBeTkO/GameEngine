using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Core;
using GameEngine.Render.Texture;
using GameEngine.Core.Collisions;
using System.Drawing;

namespace GameEngine.Render
{
    public class Camera
    {
        public Box Frame;

        public Bitmap DrawFrame(Size resolution)
        {
            var frame = new Bitmap(resolution.Width, resolution.Height);
            var g = Graphics.FromImage(frame);
            g.TranslateTransform((float)(Frame.Location.X - Frame.Width / 2), (float)(Frame.Location.Y - Frame.Height / 2));
            g.ScaleTransform(resolution.Width / Frame.Width, resolution.Height / Frame.Width);
        }
    }
}
