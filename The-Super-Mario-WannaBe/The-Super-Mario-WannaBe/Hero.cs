using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Hero
    {
        public enum DIRECTION
        {
            Left,
            Right,
            Up,
            None
        }
        public RectangleF Character { get; set; }
        public bool Dead { get; set; }
        public static readonly Image GameOverMessage = Properties.Resources.GameOverMessage;

        public Hero()
        {
            Character = new RectangleF((int) (5.8 * Level1.GenericBlock1.Width), Level1.GenericBlock1.Height, Level1.GenericBlock1.Width / 2, Level1.GenericBlock1.Height / 2);
            Dead = false;
        }

        public void Draw(Graphics g)
        {
            if (!Dead)
            {
                g.FillRectangle(new SolidBrush(Color.Black), Character);
            }
            else
            {
                g.DrawImage(GameOverMessage, new Point(175, 200));
            }
        }

        public void Fall()
        {
            float y = Character.Y + 3.0f;
            float x = Character.X;
            float width = Character.Width;
            float height = Character.Height;

            Character = new RectangleF(x, y, width, height);
        }
        
        private RectangleF MoveLeft()
        {
            float x = Character.X - 1.5f;
            float y = Character.Y;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        private RectangleF MoveUp()
        {
            float x = Character.X;
            float y = Character.Y - 3.0f;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        private RectangleF MoveRight()
        {
            float x = Character.X + 1.5f;
            float y = Character.Y;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        public void Move(DIRECTION direction)
        {
            if (direction.Equals(DIRECTION.Left))
            {
                Character = MoveLeft();
            }
            if (direction.Equals(DIRECTION.Right))
            {
                Character = MoveRight();
            }
            if (direction.Equals(DIRECTION.Up))
            {
                Character = MoveUp();
            }
        }
    }
}
