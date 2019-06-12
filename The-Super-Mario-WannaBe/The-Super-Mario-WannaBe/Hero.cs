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
        public Rectangle Character { get; set; }
        public bool Dead { get; set; }

        public enum DIRECTION
        {
            Left,
            Right,
            Up,
            None
        }

        public Hero()
        {
            Character = new Rectangle((int) (5.8 * Level1.GenericBlock1.Width), Level1.GenericBlock1.Height, Level1.GenericBlock1.Width / 2, Level1.GenericBlock1.Height / 2);
            Dead = false;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), Character);
        }

        public void Fall()
        {
            int y = Character.Y + 1;
            int x = Character.X;
            int width = Character.Width;
            int height = Character.Height;

            Character = new Rectangle(x, y, width, height);
        }
        
        private Rectangle MoveLeft()
        {
            int x = Character.X - 1;
            int y = Character.Y;
            int width = Character.Width;
            int height = Character.Height;

            return new Rectangle(x, y, width, height);
        }

        private Rectangle MoveUp()
        {
            int x = Character.X;
            int y = Character.Y - 1;
            int width = Character.Width;
            int height = Character.Height;

            return new Rectangle(x, y, width, height);
        }

        private Rectangle MoveRight()
        {
            int x = Character.X + 1;
            int y = Character.Y;
            int width = Character.Width;
            int height = Character.Height;

            return new Rectangle(x, y, width, height);
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
