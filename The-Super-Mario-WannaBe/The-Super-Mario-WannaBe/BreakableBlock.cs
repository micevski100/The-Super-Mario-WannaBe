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
            Random r = rand;
            double Angle = r.NextDouble() * 2 * Math.PI;
            VelocityX = (float)(Math.Cos(Angle) * 10);
            VelocityY = (float)(Math.Sin(Angle) * 10);

            NewImage = new Bitmap(Image);
        }

        public void ChangeBitmap(Image Image)
        {
            NewImage = new Bitmap(Image);
            this.Image = Image;
        }

        private void CheckPosition()
        {
            if (Position.Y < 0 || Position.Y > Level6.FormHeight)
            {
                InForm = false;
            }
        }

        public void CheckMoonCollision(Moon moon)
        {
            if (moon.Intersects(Bounds))
            {
                IsActive = true;
            }
        }

        public void Update()
        {
            Move();
            CheckPosition();
        }

        private void Move()
        {
            if (IsActive && InForm)
            {
                Position = new Point((int)(Position.X + VelocityX), (int)(Position.Y + VelocityY));
                NewImage = Level6.RotateImage(Image, SpinningRate, true, false, Color.Transparent);
                SpinningRate += SpinningRate;
                SpinningRate %= 360;
            }
        }

        public void Draw(Graphics g)
        {
            //if (IsActive && InForm)
            if (InForm)
            {
                g.DrawImage(NewImage, Position);
            }
        }
    }
}
