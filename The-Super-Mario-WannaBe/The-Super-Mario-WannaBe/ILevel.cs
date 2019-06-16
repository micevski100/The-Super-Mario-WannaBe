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

        public static readonly int jumpSize = 30; //jumpsize;
        int currentJump = 0;
        bool isDoubleJumping = false;
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

        public void GravityPull()
        {
            if (currentJump == 0 && HeroIsInAir())
            {
                Hero.Fall();
            }
        }

        public void Update(bool[] arrows, bool space)
        {
            bool leftArrow = arrows[0];
            bool rightArrow = arrows[1];

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
                Hero.Move(Hero.DIRECTION.Up);
                --currentJump;
            }

            // GravityPull(); doesn't work why?

            ValidateVerticalPosition(); // check later..
            
            GravityPull(); // works why ?

            if (leftArrow)
            {
                Hero.Move(Hero.DIRECTION.Left);
            }
            else if (rightArrow)
            {
                Hero.Move(Hero.DIRECTION.Right);
            }

            ValidateHorizontalPosition();

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
                // you hit your lowest point
                // 
                bool leftCollision = Collisions(Hero.Character, boundary)[3];
                bool belowCollision = Collisions(Hero.Character, boundary)[1];
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
                bool belowCollision = Collisions(Hero.Character, boundary)[1];
                if (rightCollision && Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }
            return true;
        }

        public void ValidateVerticalPosition()
        {
            // if hit od gore (ili dole?) currentJump=0
            // if hit od dole isDoubleJumping = false

            foreach(Rectangle boundary in Boundaries)
            {
                bool collisionAbove = Collisions(Hero.Character, boundary)[0];
                if (collisionAbove && Hero.Character.IntersectsWith(boundary))
                {
                    currentJump = 0;
                    Hero.Character = new RectangleF(Hero.Character.X, boundary.Bottom + 0.1f, Hero.Character.Width, Hero.Character.Height);
                    break; // might bug, check out later
                }

                bool collisionBelow = Collisions(Hero.Character, boundary)[1];
                if (collisionBelow && Hero.Character.IntersectsWith(boundary))
                {
                    isDoubleJumping = false;
                    Hero.Character = new RectangleF(Hero.Character.X, boundary.Top - 0.5f - Hero.Character.Height, Hero.Character.Width, Hero.Character.Height);
                    break; // read above
                }
            }
        }

        public void ValidateHorizontalPosition()
        {
            foreach (Rectangle boundary in Boundaries)
            {
                bool collisionBelow = Collisions(Hero.Character, boundary)[1];

                // !collisionBelow - oti ako stoo na shipkata i izleze na samio rab odma go pomestuva
                // znaci mora da se provere dali stoo na boundary

                bool collisionRight = Collisions(Hero.Character, boundary)[2];
                if (collisionRight && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Left - 0.1f - Hero.Character.Width, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break; // might bug, check out later
                }

                bool collisionLeft = Collisions(Hero.Character, boundary)[3];
                if (collisionLeft && Hero.Character.IntersectsWith(boundary) && !collisionBelow)
                {
                    Hero.Character = new RectangleF(boundary.Right + 0.1f, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                    break; // read above
                }
            }
        }
    }
}
