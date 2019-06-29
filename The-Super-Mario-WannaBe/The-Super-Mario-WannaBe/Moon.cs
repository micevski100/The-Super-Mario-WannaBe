using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Moon
    {
        public static Image Image = Properties.Resources.Moon;
        public Point Center { get; set; }
        public static int Radius;
        public float Speed { get; set; }
        public static int Floor;
        public static int LeftWall;
        public static int RightWall;
        public bool hitFloor;
        public bool hitLeftWall;

        public Moon(Point Center)
        {
            this.Center = Center;
            Radius = 15;
            Speed = 15;

            Floor = Level6.FormHeight - 15 * Level6.GenericBlock1.Height;
            LeftWall = 0;
            RightWall = Level6.FormWidth - Level6.GenericBlock1.Width;
            hitFloor = false;
            hitLeftWall = false;
        }

        private void MoveLeft()
        {
            // to do
            Center = new Point(Center.X - 1, Center.Y);
        }

        private void MoveDown()
        {
            // to do
            Center = new Point(Center.X, Center.Y + 1);
        }

        private void MoveUpRight()
        {
            Center = new Point(Center.X + 2, Center.Y - 1);
        }

        public void Move()
        {
            if (Center.Y + Radius >= Floor && !hitFloor)
            {
                hitFloor = true;
            }

            if (Center.X - Radius <= LeftWall && !hitLeftWall)
            {
                hitLeftWall = true;
            }

            if (!hitFloor)
            {
                MoveDown();
            }
            else if (!hitLeftWall)
            {
                MoveLeft();
            }
            else
                MoveUpRight();

        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        // may work idk
        bool intersects(RectangleF rect)
        {
            Point circleDistance = new Point();
            
            circleDistance.X = (int)Math.Abs(Center.X - rect.X);
            circleDistance.Y = (int)Math.Abs(Center.Y - rect.Y);

            if (circleDistance.X > (rect.Width / 2 + Radius)) { return false; }
            if (circleDistance.Y > (rect.Height / 2 + Radius)) { return false; }

            if (circleDistance.X <= (rect.Width / 2)) { return true; }
            if (circleDistance.Y <= (rect.Height / 2)) { return true; }

            int cornerDistance_sq = (int)((circleDistance.X - rect.Width / 2) * (circleDistance.X - rect.Width / 2) +
                                 (circleDistance.Y - rect.Height / 2) * (circleDistance.Y - rect.Height / 2) );

            return (cornerDistance_sq <= (Radius * Radius));
        }

        public void Draw(Graphics g)
        {
            int x = Center.X - Radius;
            int y = Center.Y - Radius;
            g.DrawImage(Image, new Point(x, y));
        }
    }
}
