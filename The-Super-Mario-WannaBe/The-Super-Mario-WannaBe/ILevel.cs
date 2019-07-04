using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    abstract public class Level
    {
        public List<Rectangle> Boundaries { get; set; }
        public Hero Hero { get; set; }
        public int JumpSize = 30;
        public int CurrentJump = 0;
        public bool isDoubleJumping = false;


        /* Checks collisions with enviorment */
        public bool[] Collisions(RectangleF hero, Rectangle rectangle)
        {
            bool hitSomethingAbove = false;
            bool hitSomethingBelow = false;
            bool hitSomethingOnTheRight = false;
            bool hitSomethingOnTheLeft = false;

            if (hero.Top < rectangle.Bottom && rectangle.Bottom < hero.Bottom) // hero hit something above him
            {
                hitSomethingAbove = true;
            }

            if (hero.Bottom > rectangle.Top && rectangle.Top > hero.Top) // hero stands above something
            {
                hitSomethingBelow = true;
            }

            if (hero.Right > rectangle.Left && rectangle.Left > hero.Left) // hero hit something on the right
            {
                hitSomethingOnTheRight = true;
            }

            if (hero.Left < rectangle.Right && rectangle.Right < hero.Right) // hero hit something on the left
            {
                hitSomethingOnTheLeft = true;
            }

            return new bool[] { hitSomethingAbove, hitSomethingBelow, hitSomethingOnTheRight, hitSomethingOnTheLeft };
        }

        public bool HeroIsInAir()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionBelow = Collisions(Hero.Character, boundary)[1];

                if (collisionBelow && Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }

            return true;
        }

        /* Sets the OnGround variable from the hero class */
        private void Check()
        {
            if (HeroIsInAir())
                Hero.OnGround = false;
            else
                Hero.OnGround = true;
        }

        private void ValidateVerticalPosition()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionAbove = Collisions(Hero.Character, boundary)[0];
                if (collisionAbove && Hero.Character.IntersectsWith(boundary))
                {
                    CurrentJump = 0;
                    Hero.Character = new RectangleF(Hero.Character.X, boundary.Bottom + 0.1f, Hero.Character.Width, Hero.Character.Height);
                    break;
                }

                bool collisionBelow = Collisions(Hero.Character, boundary)[1];
                if (collisionBelow && Hero.Character.IntersectsWith(boundary))
                {
                    isDoubleJumping = false;
                    Hero.Character = new RectangleF(Hero.Character.X, boundary.Top - 0.5f - Hero.Character.Height, Hero.Character.Width, Hero.Character.Height);
                    break;
                }
            }
        }

        private void ValidateHorizontalPosition()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionLeft = Collisions(Hero.Character, boundary)[3];
                bool collisionRight = Collisions(Hero.Character, boundary)[2];
                bool collisionBelow = Collisions(Hero.Character, boundary)[1];

                if (collisionRight && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Left - 0.1f - Hero.Character.Width, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break;
                }

                if (collisionLeft && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Right + 0.1f, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break;
                }
            }
        }

        public void GravityPull()
        {
            if (CurrentJump == 0 && HeroIsInAir())
            {
                Hero.Fall();
            }
        }

        public virtual void Update(bool[] arrows, bool space)
        {
            bool moved = false;
            bool leftArrow = arrows[0];
            bool rightArrow = arrows[1];
            Check();

            if (space)
            {
                if (!HeroIsInAir())
                {
                    CurrentJump = JumpSize;
                }
                else
                {
                    if (!isDoubleJumping)
                    {
                        CurrentJump += JumpSize / 2;
                        isDoubleJumping = true;
                    }
                }
            }

            if (CurrentJump > 0)
            {
                moved = true;
                Hero.Move(Hero.DIRECTION.Up);
                --CurrentJump;
            }

            ValidateVerticalPosition();

            GravityPull();

            if (leftArrow)
            {
                moved = true;
                Hero.Move(Hero.DIRECTION.Left);
            }
            else if (rightArrow)
            {
                moved = true;
                Hero.Move(Hero.DIRECTION.Right);
            }
            if (!moved)
            {
                Hero.ResetFrame();
            }

            ValidateHorizontalPosition();
        }

        public abstract int ChangeLevel();

        public abstract void Draw(Graphics g);
    }
}
