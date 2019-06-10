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

        abstract public void Draw(Graphics g);
        // list floors
        // list walls
        // list cellings

            // woohoo yay me slusate li da jas ne ve slusam, kak be idk, c da 
        /*public nesto ime(Rectangle hero, Rectangle rectangle)
        {
            if (hero.Top < rectangle.Bottom && rectangle.Bottom < hero.Bottom)
            {
                //se udril od gore(so glavata udril gornio dzid)
                // veke na gore da ne ode
            }
            if (hero.Bottom > rectangle.Top && rectangle.Top > hero.Top)
            {
                //se udril od dole(stoo vrz nesto)
                // veke na dole da ne ode
            }
            if (hero.Right > rectangle.Left && rectangle.Left > hero.Left)
            {
                //se udril od desno
                // veke na desno da ne ode
            }
            if (hero.Left < rectangle.Right && rectangle.Right < hero.Right)
            {
                //se udril od levo
                // veke na levo da ne ode
            }
            //ne see udril
            // deka saka neka ode
        }*/

        public bool HeroIsInAir(Hero Hero, List<Rectangle> Boundaries) 
        {
            foreach (Rectangle boundary in Boundaries)
            {
                if (Hero.Character.IntersectsWith(boundary))
                {
                    return false;
                }
            }
            return true;
        }

        public void Update(bool[] arrows, bool space)
        {

        }

        public void GravityPull(Hero Hero, List<Rectangle> Boundaries)
        {
            if (HeroIsInAir(Hero, Boundaries))
            {
                Hero.Fall();
            }
        }

        public int NumberOfIntersects(Hero Hero, List<Rectangle> Boundaries)
        {
            int count = 0;

            foreach(Rectangle boundary in Boundaries)
            {
                if (Hero.Character.IntersectsWith(boundary))
                {
                    ++count;
                }
            }

            return count;
        }

        
    }
}
