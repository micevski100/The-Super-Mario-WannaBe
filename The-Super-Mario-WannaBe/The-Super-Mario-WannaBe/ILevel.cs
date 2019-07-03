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
        public Hero Hero { get; set; }
        public List<Rectangle> Boundaries { get; set; }
        public int jumpSize = 30; //jumpsize;
        public int currentJump = 0;
        public bool isDoubleJumping = false;
        abstract public void Draw(Graphics g);

        public bool[] Collisions(RectangleF hero, Rectangle rectangle)
        {
            bool hitSomethingAbove = false;
            bool hitSomethingBelow = false;
            bool hitSomethingOnTheRight = false;
            bool hitSomethingOnTheLeft = false;

            if (hero.Top < rectangle.Bottom && rectangle.Bottom < hero.Bottom) // hero hit something above him
            {
                //se udril od gore(so glavata udril gornio dzid)
                // veke na gore da ne ode
                hitSomethingAbove = true;
            }
            if (hero.Bottom > rectangle.Top && rectangle.Top > hero.Top) // hero stands above something
            {
                //se udril od dole(stoo vrz nesto)
                // veke na dole da ne ode
                hitSomethingBelow = true;
            }
            if (hero.Right > rectangle.Left && rectangle.Left > hero.Left) // hero hit something on the right
            {
                //se udril od desno
                // veke na desno da ne ode
                hitSomethingOnTheRight = true;
            }
            if (hero.Left < rectangle.Right && rectangle.Right < hero.Right) // hero hit something on the left
            {
                //se udril od levo
                // veke na levo da ne ode
                hitSomethingOnTheLeft = true;
            }
            //ne see udril
            // deka saka neka ode

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

        private void check()
        {
            if (HeroIsInAir())
                Hero.OnGround = false;
            else
                Hero.OnGround = true;
        }

        public void GravityPull()
        {
            if (currentJump == 0 && HeroIsInAir())
            {
                Hero.Fall();
            }
        }

        public virtual void Update(bool[] arrows, bool space)
        {
            bool moved = false;
            bool leftArrow = arrows[0];
            bool rightArrow = arrows[1];
            check();

            if (space)
            {
                if (!HeroIsInAir())
                {
                    currentJump = jumpSize;
                }
                else
                {
                    if (!isDoubleJumping)
                    {
                        currentJump += jumpSize / 2;
                        isDoubleJumping = true;
                    }
                }
            }

            if (currentJump > 0)
            {
                moved = true;
                Hero.Move(Hero.DIRECTION.Up);
                --currentJump;
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

        public void ValidateVerticalPosition()
        {
            foreach(Rectangle boundary in Boundaries)
            {
                bool collisionAbove = Collisions(Hero.Character, boundary)[0];
                if (collisionAbove && Hero.Character.IntersectsWith(boundary))
                {
                    currentJump = 0;
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

        public void ValidateHorizontalPosition()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionBelow = Collisions(Hero.Character, boundary)[1];

                bool collisionRight = Collisions(Hero.Character, boundary)[2];
                if (collisionRight && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Left - 0.1f - Hero.Character.Width, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break;
                }

                bool collisionLeft = Collisions(Hero.Character, boundary)[3];
                if (collisionLeft && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Right + 0.1f, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break;
                }
            }
        }
    }
}
