using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class BreakableBlock
    {
        public static Random rand = new Random();

        public Bitmap NewImage { get; set; }
        public Image Image { get; set; }
        public Rectangle Bounds { get; set; }
        public bool IsActive { get; set; }
        public bool InForm { get; set; }
        public float SpinningRate { get; set; }
        public Point Position { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public BreakableBlock(Rectangle Bounds, Image Image)
        {
            this.Bounds = Bounds;
            this.Image = Image;
            InForm = true;
            IsActive = false;

            if (Image == Level6.GenericBlock1)
            {
                SpinningRate = 0;
            }
            else
            {
                SpinningRate = (float)(rand.NextDouble() * 360);
            }
            Position = new Point(Bounds.X, Bounds.Y);
            Random r = new Random();
            double Angle = r.NextDouble() * 2 * Math.PI;
            VelocityX = (float)(Math.Cos(Angle) * 10);
            VelocityY = (float)(Math.Sin(Angle) * 10);
        }

        private void CheckPosition()
        {
            if (Position.Y < 0 || Position.Y > Level6.FormHeight)
            {
                InForm = false;
            }
        }

        private void CheckMoonCollision(Moon moon)
        {
            //to-do
        }

        public void Update(Hero hero)
        {
            //unfinished
            CheckPosition();
            Move();
        }

        private void Move()
        {
            if (IsActive && InForm)
            {
                Position = new Point((int)(Position.X + VelocityX), (int)(Position.Y + VelocityY));
                NewImage = Level6.RotateImage(Image, SpinningRate, true, false, Color.Transparent);
                SpinningRate += SpinningRate;
            }
        }

        public void Draw(Graphics g)
        {
            if (IsActive && InForm)
            {
                g.DrawImage(NewImage, Position);
            }
        }
    }
}
