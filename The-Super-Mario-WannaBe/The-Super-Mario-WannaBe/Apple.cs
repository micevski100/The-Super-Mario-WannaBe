using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Apple
    {
        public enum TypeOfApple
        {
            Flying, Falling
        }
        public static readonly Image GenericApple = Properties.Resources.apple;
        public Rectangle Bounds { get; set; }
        public Rectangle TriggerArea { get; set; }
        public TypeOfApple Type { get; set; }
        public bool IsActive { get; set; }
        public bool InForm { get; set; }

        public Apple(int X, int Y, Rectangle TriggerArea, TypeOfApple Type)
        {
            this.Bounds = new Rectangle(X, Y, GenericApple.Width, GenericApple.Height);
            this.TriggerArea = TriggerArea;
            this.Type = Type;
            IsActive = false;
            InForm = true;
        }

        private void CheckTriggerArea(Hero Hero)
        {
            if (Hero.Character.IntersectsWith(TriggerArea))
            {
                IsActive = true;
            }
        }

        public void Draw(Graphics g)
        { 
            g.DrawImage(GenericApple, Bounds);
        }

        public void Update(Hero Hero)
        {
            CheckTriggerArea(Hero);
            Move();
        }

        private void CheckPosition()
        {
            if (Bounds.Bottom < 0 || Bounds.Top > Level2.FormHeight)
            {
                InForm = false;
            }
        }

        private void Move()
        {
            if (IsActive)
            {
                if (Type == TypeOfApple.Falling)
                {
                    MoveDown();
                }
                else
                {
                    MoveUp();
                }

                CheckPosition();
            }
        }

        private void MoveUp()
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y - 5, Bounds.Width, Bounds.Height);
        }

        private void MoveDown()
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y + 5, Bounds.Width, Bounds.Height);
        }

        public void CheckCollision(Hero Hero)
        {
            if (Hero.Character.IntersectsWith(Bounds))
            {
                Hero.Dead = true;
            }
        }
    }
}
