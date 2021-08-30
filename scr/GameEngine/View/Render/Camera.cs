using System;
using System.Collections.Generic;
using System.Text;
using GameEngine.Logic;
using GameEngine.View.Render.Texture;
using System.Linq;
using GameEngine.Logic.Collisions;
using System.Drawing;

namespace GameEngine.View.Render
{
    public class Camera
    {
        public Box Frame;

        public Bitmap DrawFrame(Size resolution)
        {
            var frame = new Bitmap(resolution.Width, resolution.Height);
            var g = Graphics.FromImage(frame);
            g.ScaleTransform(resolution.Width / (float)Frame.Width, -resolution.Height / (float)Frame.Height);
            g.TranslateTransform(-(float)(Frame.Location.X - Frame.Width / 2), -(float)(Frame.Location.Y - Frame.Height / 2) - (float)Frame.Height);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            foreach (var obj in Core.Objects.Where(o => o.Visible).OrderBy(o => o.DrawPriority))
            {
                var texture = obj.GetTexture();
                var pos = ConvertVector(obj.Body.Location - new Vector(texture.Width / 2, texture.Height / 2), texture.Height);
                g.DrawImage(texture.Image, new RectangleF(pos, new SizeF(texture.Width, -texture.Height)));
            }
            g.Dispose();
            return frame;
        }

        static PointF ConvertVector(Vector v, double height)
        {
            return new PointF((float)v.X, (float)v.Y + (float)height);
        }
    }
}
