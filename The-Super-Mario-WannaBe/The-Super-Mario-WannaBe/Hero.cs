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
        
        public enum DIRECTION
        {
            Left,
            Right,
            None
        }


        public Hero()
        {
            Character = new Rectangle((int) (5.8 * Level1.GenericBlock1.Width), Level1.GenericBlock1.Height, Level1.GenericBlock1.Width / 2, Level1.GenericBlock1.Height / 2);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), Character);
        }

        public void Fall()
        {
            int y = Character.Y + 2;
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
            int[] dir = { 0, 0 };
            if (direction.Equals(DIRECTION.Left))
            {
                dir[0] -= 1;
                Character = MoveLeft();
            }
            if (direction.Equals(DIRECTION.Right))
            {
                dir[0] += 1;
                Character = MoveRight();
            }
        }
    }
}
