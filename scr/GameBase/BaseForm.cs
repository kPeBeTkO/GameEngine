using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GameEngine.View.Render;
using GameEngine.Logic;

namespace GameBase
{
    public class BaseForm : Form
    {
        public Camera Cam;
        public Rectangle FrameLocation;
        public Timer Timer;
        public readonly int TPS;

        public BaseForm(int tps)
        {
            DoubleBuffered = true;
            TPS = tps;
            Timer = new Timer();
            Timer.Interval = 1000 / tps;
            Paint += (s, a) =>
            {
                Graphics g = a.Graphics;
                RenderBack(a.Graphics);
                RenderGame(a.Graphics);
                RenderGui(a.Graphics);
            };
            Cam = new Camera();
            Timer.Tick += (s, a) => 
            {
                Core.Update();
                Invalidate();
            };
            KeyDown += (s, a) => Core.Input(new KeyInput(a.KeyCode.ToString()));
            KeyUp += (s, a) => Core.Input(new KeyInput(a.KeyCode.ToString(), true));
            MouseDown += (s, a) => Core.Input(new KeyInput("Mouse" + a.Button.ToString()));
            MouseUp += (s, a) => Core.Input(new KeyInput("Mouse" + a.Button.ToString(), true));

            MouseMove += (s, a) =>
            {
                var offset = new Vector(a.X - FrameLocation.X - FrameLocation.Width / 2,  (a.Y - FrameLocation.Y) - FrameLocation.Height / 2);
                var scaledOffset = new Vector(offset.X * (Cam.Frame.Width / FrameLocation.Width), -offset.Y * (Cam.Frame.Height / FrameLocation.Height));
                Core.MouseLocation = Cam.Frame.Location + scaledOffset;
            };
            Timer.Start();
        }

        public virtual void RenderBack(Graphics graphics)
        {

        }

        public virtual void RenderGame(Graphics graphics)
        {
            FrameLocation = new Rectangle(0, 0, Width, Height);
            var frame = Cam.DrawFrame(new Size(Width, Height));
            graphics.DrawImage(frame, FrameLocation);
        }

        public virtual void RenderGui(Graphics graphics)
        {

        }
    }

}

