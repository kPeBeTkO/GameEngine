using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Core;
using GameEngine.Render.Texture;
using System.Linq;
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
            g.ScaleTransform(resolution.Width / Frame.Width, -resolution.Height / Frame.Height);
            g.TranslateTransform(-(float)(Frame.Location.X - Frame.Width / 2), -(float)(Frame.Location.Y - Frame.Height / 2) - Frame.Height);
            foreach (var obj in Core.Core.Objects)
            {
                var texture = obj.GetTexture();
                var pos = ConvertVector(obj.Body.Location - new Vector(texture.Size.Width / 2, texture.Size.Height / 2));
                g.DrawImage(texture.Image, new RectangleF(pos, texture.Size));
            }
            g.Dispose();
            return frame;
        }

        static PointF ConvertVector(Vector v)
        {
            return new PointF((float)v.X, (float)v.Y);
        }
    }
}
