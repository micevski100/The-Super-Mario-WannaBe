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
        public float Spin { get; set; }
        public bool hitFloor;
        public bool hitLeftWall;
        public bool hitLeftWall1;
        public int LeftWall { get; set; }
        public bool IsActive { get; set; }
        public int Timer { get; set; }

        public Bitmap NewImage { get; set; }

        public Moon(Point Center)
        {
            this.Center = Center;
            Radius = Image.Size.Width / 2;
            Spin = 1f;
            LeftWall = Radius + 40;
            hitFloor = false;
            hitLeftWall = false;
            hitLeftWall1 = false;
            IsActive = false;
            Timer = 0;

            NewImage = new Bitmap(Image);
        }

        private void CheckTrigger(Hero hero)
        {
            if ( hero.Character.X > Level6.FormWidth - 7 * Level6.GenericBlock1.Width)
            {
                IsActive = true;
            }
        }

        private void MoveUpLeft()
        {
            if (Center.X < 0)
            {
                hitLeftWall1 = true;
            }
            else
            {
                Center = new Point(Center.X - 1, Center.Y - 3);
            }
        }

        private void MoveLeft()
        {
            // to do
            if (Center.X < LeftWall)
            {
                hitLeftWall = true;
            }
            else
            {
                Center = new Point(Center.X - 1, Center.Y);
            }
        }

        private void MoveDown(List<Rectangle> Boundaries)
        {
            bool flag = false;
            for (int i = 0; i < 7; i++)
            {
                if (Intersects(Boundaries[i]))
                {
                    flag = true;
                    hitFloor = true;
                    break;
                }
            }

            if (!flag)
            {
                Center = new Point(Center.X, Center.Y + 5);
            }
        }

        private void MoveUpRight()
        {
            if (Center.X > Level6.FormWidth - 40)
            {
                hitFloor = false;
                hitLeftWall = false;
                hitLeftWall1 = false;
            }
            else
            {
                Center = new Point(Center.X + 10, Center.Y - 1);
            }
        }

        public void Move(List<Rectangle> Boundaries, Hero hero)
        {
            CheckTrigger(hero);
            if (IsActive)
            {
                if (!hitLeftWall1)
                {
                    MoveDown(Boundaries);
                }

                if (hitFloor)
                {
                    MoveLeft();
                }

                if (hitLeftWall)
                {
                    MoveUpLeft();
                }

                if (hitLeftWall1)
                {
                    MoveUpRight();
                }

                CheckHeroPosition(hero);
            }
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public bool Intersects(RectangleF rectangle)
        {
            PointF circleDistance = new PointF();
            RectangleF rect = new RectangleF(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2, rectangle.Width, rectangle.Height);

            circleDistance.X = Math.Abs(Center.X - rect.X);
            circleDistance.Y = Math.Abs(Center.Y - rect.Y);

            if (circleDistance.X > (rect.Width / 2 + Radius)) { return false; }
            if (circleDistance.Y > (rect.Height / 2 + Radius)) { return false; }

            if (circleDistance.X <= (rect.Width / 2)) { return true; }
            if (circleDistance.Y <= (rect.Height / 2)) { return true; }

            float cornerDistance_sq = ((circleDistance.X - rect.Width / 2) * (circleDistance.X - rect.Width / 2) +
                                 (circleDistance.Y - rect.Height / 2) * (circleDistance.Y - rect.Height / 2));

            return cornerDistance_sq <= (Radius * Radius);
        }

        private void CheckHeroPosition(Hero hero)
        {
            if (Intersects(hero.Character))
            {
                hero.Dead = true;
            }
        }

        public void Draw(Graphics g)
        {
            if (IsActive)
            {
                int x = Center.X - Radius;
                int y = Center.Y - Radius;
                g.DrawImage(NewImage, new Point(x, y));

                Timer++;

                if (Timer % 6 == 0)
                {
                    NewImage = Level6.RotateImage(Image, Spin += 10, false, true, Color.Transparent);
                }
            }
        }
    }
}
