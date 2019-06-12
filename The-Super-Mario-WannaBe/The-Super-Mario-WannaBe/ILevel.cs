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

        public static readonly int jumpSize = 10; //jumpsize;
        int currentJump = 0;
        bool isDoubleJumping = false;
        abstract public void Draw(Graphics g);

        public bool[] Collisions(Rectangle hero, Rectangle rectangle)
        {
            bool hitSomethingAbove = false;
            bool hitSomethingBelow = false;
            bool hitSomethingOnTheRight = false;
            bool hitSomethingOnTheLeft = false;

            if (hero.Top < rectangle.Bottom + 1 && rectangle.Bottom + 1 < hero.Bottom) // hero hit something above him
            {
                //se udril od gore(so glavata udril gornio dzid)
                // veke na gore da ne ode
                hitSomethingAbove = true;
            }
            if (hero.Bottom > rectangle.Top - 1 && rectangle.Top - 1 > hero.Top) // hero stands above something
            {
                //se udril od dole(stoo vrz nesto)
                // veke na dole da ne ode
                hitSomethingBelow = true;
            }
            if (hero.Right > rectangle.Left - 1 && rectangle.Left - 1 > hero.Left) // hero hit something on the right
            {
                //se udril od desno
                // veke na desno da ne ode
                hitSomethingOnTheRight = true;
            }
            if (hero.Left < rectangle.Right + 1 && rectangle.Right + 1 < hero.Right) // hero hit something on the left
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

        public void GravityPull()
        {
            if (currentJump == 0 && HeroIsInAir())
            {
                Hero.Fall();
            }
            if (!HeroIsInAir())
            {
                //isDoubleJumping = false;
            }
        }

        public void Update(bool[] arrows, bool space)
        {
            bool leftArrow = arrows[0];
            bool rightArrow = arrows[1];

            if (leftArrow && HeroCanMoveLeft())
            {
                Hero.Move(Hero.DIRECTION.Left);
            }
            else if (rightArrow && HeroCanMoveRight())
            {
                Hero.Move(Hero.DIRECTION.Right);
            }

            /*
             * bool[] space
             * 
             * if (space[0] && !space[1])
             *  currentJump = jumpsize
             * else if (space[0] && space[1])
             *  currentJump += nesto
             */

            if (space)
            {
                if (!HeroIsInAir())
                {
                    currentJump = jumpSize;
                    isDoubleJumping = true;
                }
                else
                {
                    if (isDoubleJumping) // it doesnt work.. why ? | it works.. why?
                    {
                        currentJump += jumpSize;
                        //isDoubleJumping = false;
                    }
                }
            }

            if (currentJump > 0)
            {
                if (!HeroCanMoveUp())
                {
                    currentJump = 0;
                }
                else
                {
                    Hero.Move(Hero.DIRECTION.Up);
                    --currentJump;
                }
            }

            GravityPull();
        }

            private bool HeroCanMoveUp()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionAbove = Collisions(Hero.Character, boundary)[0];
                if (collisionAbove && Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HeroCanMoveLeft()
        {
            foreach(Rectangle boundary in Boundaries)
            {
                bool leftCollision = Collisions(Hero.Character, boundary)[3];
                if (leftCollision && Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HeroCanMoveRight()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool rightCollision = Collisions(Hero.Character, boundary)[2];
                if (rightCollision && Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
